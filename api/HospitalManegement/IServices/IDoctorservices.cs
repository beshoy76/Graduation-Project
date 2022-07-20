using HospitalManegement.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Modelsidentiy.Shared;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace HospitalManegement.IServices
{
  
    public interface IDoctorservices
    {
        Task<authmodel> RegesterDoctor(RegisterDoctorviewModel model);
        Task<authmodel> LoginDoctor(LoginViewModel model);
        Task<string> BookingDoctor(string id, string patientId);
        string GetUserid();
        Task<List<BookingDoctor>> Doctorsyou(string PatientId);
        Task<List<BookingDoctor>> patientsdoctor(string DoctorId);
        Task<List<Doctor>> doctorInfo();
        Task<List<Patient>> patientInfo();
      //  Task<string> getImageDoctor();
      //  Doctor postDoctorImage(Doctor doctor, HttpPostedFileBase photo );
      

        //attending 

        bool IsAUthenticated();
       
    }
    public class doctorservice : IDoctorservices
    {
        private readonly HositalContext context;
        private readonly UserManager<ApplicationUser> usermanger;
        private IConfiguration configuration;
        private readonly IOptions<Jwt> jwt;
        private readonly IHttpContextAccessor httpContext;
        private readonly RoleManager<IdentityRole> roleManager;

        private readonly Jwt _jwt;
        public doctorservice(UserManager<ApplicationUser> usermanger, IConfiguration configuration,
              IOptions<Jwt> jwt, IHttpContextAccessor httpContext, RoleManager<IdentityRole> roleManager, HositalContext context)
        {
            this.context = context;
            this.usermanger = usermanger;
            this.configuration = configuration;
            this.jwt = jwt;
            this.httpContext = httpContext;
            this.roleManager = roleManager;

            _jwt = jwt.Value;
        }
        public string GetUserid()
        {

            return httpContext.HttpContext.User?.FindFirstValue(ClaimTypes.NameIdentifier);
        }
        public bool IsAUthenticated()
        {
            return httpContext.HttpContext.User.Identity.IsAuthenticated;
        }
        public async Task<authmodel> RegesterDoctor(RegisterDoctorviewModel model)
        {
            if (model == null)
                return new authmodel
                {
                    Message = "Please enter all required data",
                    IsAuthenticated = false
                };
            if (!model.Email.Contains('@'))
            {
                return new authmodel
                {
                    Message = "please enter email correct!! must contain @",
                    IsAuthenticated = false
                };
            }
            if (model.Passward == null ||model.Passward.Length<=6)
            {
                return new authmodel
                {
                    Message = "please enter password correct!! must contain more than 6 letters and symbols such as @#_",
                    IsAuthenticated = false
                };

            }


            if (model.Passward != model.Confirmpassward)
                return new authmodel
                {
                    Message = "confirm passward not equal passward",
                    IsAuthenticated = false
                };


            var dept = context.Departments.Find(model.departmentid);
            var hospital = context.Hospitals.Find(model.Hospitalid);
            var user = new ApplicationUser
            {

                Email = model.Email,
                UserName = model.Email,
                PhoneNumber = model.DPhone,
                doctor = new Doctor
                {
                    Day=model.day,
                    DAddress = model.DAddress,

                    name = model.DFirstName,

                    DLastName = model.DLastName,
                    id = model.Email,
                    DPass = model.Passward,
                    DAge = model.DAge,
                    DPhone = model.DPhone,
                    department = dept,
                   Hospitals=hospital



                }



            };




            var result = await usermanger.CreateAsync(user, model.Passward);

            if (!result.Succeeded)
            {
                return new authmodel
                {
                    Message = "user do not create",
                    IsAuthenticated = false,
                    // = result.Errors.Select(x => x.Description)

                };
            };


            await usermanger.AddToRoleAsync(user, "Doctor");
            var jwtSecurityToken = await CreateJwtToken(user);

            return new authmodel
            {
                Email = user.Email,
                ExpiresOn = jwtSecurityToken.ValidTo,
                IsAuthenticated = true,
                Role = new List<string> { "Doctor" },
                Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
                Username = user.UserName
            };

        }

        public async Task<authmodel> LoginDoctor(LoginViewModel model)
        {
            if (!model.Email.Contains('@'))
            {
                return new authmodel
                {
                    Message = "please enter email correct!! must contain @",
                    IsAuthenticated = false
                };
            }
            if (model.passward == null || model.passward.Length <= 6)
            {
                return new authmodel
                {
                    Message = "please enter password correct!! must contain more than 6 letters and symbols such as @#_",
                    IsAuthenticated = false
                };

            }

            var authModel = new authmodel();
            var user = await usermanger.FindByEmailAsync(model.Email);
            if (user == null)
            {
            return    new authmodel
                {
                    Message = "can not find this user email",
                    IsAuthenticated = false
                };
            };

            var result = await usermanger.CheckPasswordAsync(user, model.passward);

            if (!result)
            {
            return    new authmodel
                {
                    Message = "Password not true ",
                    IsAuthenticated = false
                };


            }

            var jwtSecurityToken = await CreateJwtToken(user);
            var rolesList = await usermanger.GetRolesAsync(user);

            authModel.IsAuthenticated = true;
            authModel.Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            authModel.Email = user.Email;
            authModel.Username = user.UserName;
            authModel.ExpiresOn = jwtSecurityToken.ValidTo;
            authModel.Role = rolesList.ToList();

            return authModel;


        }

        private async Task<JwtSecurityToken> CreateJwtToken(ApplicationUser user)
        {
            var userClaims = await usermanger.GetClaimsAsync(user);
            var roles = await usermanger.GetRolesAsync(user);
            var roleClaims = new List<Claim>();

            foreach (var role in roles)
                roleClaims.Add(new Claim("roles", role));

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("uid", user.Id)
            }
            .Union(userClaims)
            .Union(roleClaims);

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwt.Issuer,
                audience: _jwt.Audience,
                claims: claims,
                expires: DateTime.Now.AddDays(_jwt.DurationInDays),
                signingCredentials: signingCredentials);

            return jwtSecurityToken;
        }

        public async Task<string> BookingDoctor(string id, string patientId)
        {
            var patient = context.Patients.Find(patientId);
            string patientid = patient.id;
            if (patient == null)
            {

                return "you shoud log in";
            }
            var doctor = await context.Doctors
           .Where(x => x.id == id)
           .SingleOrDefaultAsync();
            var doctorid = doctor.id;



            if (doctor == null)
            {

                return "DOCTOR Not found";
            }

            var dataa = new BookingDoctor()
            {

                patientid = patientid,
                doctorid = doctorid
            };

            context.Add(dataa);
            context.SaveChanges();
            return "succefully booking";
        }

        public async Task<List<BookingDoctor>> Doctorsyou(string patientId)
        {
           

      
            var listdoctor = await context.BookingDoctors
                .Include(x => x.doctor).Where(x => x.patientid == patientId)
                .ToListAsync();


            
          
       
            return listdoctor;

        }
        public async Task<List<BookingDoctor>> patientsdoctor(string DoctorId)
        {
           


            var listPatient= await context.BookingDoctors
                .Include(x => x.patient).Where(x => x.doctorid == DoctorId)
                .ToListAsync();





            return listPatient;

        }

        public async Task<List<Doctor>> doctorInfo()
        {
            var doctorlogin = GetUserid();


            var doctor = await context.Doctors
                .Where( x => x.id == doctorlogin).ToListAsync();

            return (doctor);
        }
        public async Task<List<Patient>> patientInfo()
        {
            var patientlogin = GetUserid();


            var patient = await context.Patients
                .Where(x => x.id == patientlogin).ToListAsync();

            return (patient);
        }



       
    }
}
