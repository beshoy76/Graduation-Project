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

namespace HospitalManegement.IuserServices
{
    public interface IuserServicer
    {
      
        Task<authmodel> Regesteruserasync(RegisterViewmodel model);
        Task<authmodel> Loginuser(LoginViewModel model);
       
    }
    public class userservice : IuserServicer
    {
        private readonly HositalContext context;
        private readonly UserManager<ApplicationUser> usermanger;
        private IConfiguration configuration;
        private readonly IOptions<Jwt> jwt;
        private readonly IHttpContextAccessor httpContext;
        private readonly RoleManager<IdentityRole> roleManager;

        private readonly Jwt _jwt;
        public userservice(UserManager<ApplicationUser> usermanger, IConfiguration configuration,
              IOptions<Jwt> jwt, RoleManager<IdentityRole> roleManager, HositalContext context)
        {
            this.context = context;
            this.usermanger = usermanger;
            this.configuration = configuration;
            this.jwt = jwt;
            this.roleManager = roleManager;

            _jwt = jwt.Value;
        }




        //public string GetUserid()
        //{

        //    return httpContext.HttpContext.User?.FindFirstValue(ClaimTypes.NameIdentifier);
        //}
        //public bool IsAUthenticated()
        //{
        //    return httpContext.HttpContext.User.Identity.IsAuthenticated;
        //}



        public async Task<authmodel> Loginuser(LoginViewModel model)
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
                new authmodel
                {
                    Message = "can not find this user email",
                    IsAuthenticated = false
                };
            };

            var result = await usermanger.CheckPasswordAsync(user, model.passward);

            if (!result)
            {
                new authmodel
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


        public async Task<authmodel> Regesteruserasync(RegisterViewmodel model)
        {
            if (model == null)
                return new authmodel
                {
                    Message = "please enter correct data",
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
            if (model.Passward == null || model.Passward.Length <= 6)
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
            var user = new ApplicationUser
            {

                Email = model.Email,
                UserName = model.Email,
                PhoneNumber = model.PhoneNumper,
                patient = new Patient
                {

                    PAge = model.Age,

                    PFirstName = model.FirstName,

                    PLastName = model.LastName,
                    id = model.Email
                    



                }


            };

            var result = await usermanger.CreateAsync(user, model.Passward);

            if (!result.Succeeded)
            {
                return new authmodel
                {
                    Message = "user did not create",
                    IsAuthenticated = false,
                    // = result.Errors.Select(x => x.Description)

                };
            };


            await usermanger.AddToRoleAsync(user, "User");
            var jwtSecurityToken = await CreateJwtToken(user);

            return new authmodel
            {
                Email = user.Email,
                ExpiresOn = jwtSecurityToken.ValidTo,
                IsAuthenticated = true,
                Role = new List<string> { "User" },
                Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
                Username = user.UserName
            };

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


        //public async Task<List<Patient>> patientInfo()
        //{
        //    var patientlogin = GetUserid();


        //    var patient = await context.Patients
        //        .Where(x => x.id == patientlogin).ToListAsync();

        //    return (patient);
        //}



    }
}


