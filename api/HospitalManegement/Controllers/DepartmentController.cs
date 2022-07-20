using HospitalManegement.IServices;
using HospitalManegement.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Modelsidentiy.Shared;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace HospitalManegement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmendServices departservices;
        private readonly UserManager<ApplicationUser> usermanger;
        private readonly IDoctorservices doctorservices;
       //private readonly HositalContext context;
        [Obsolete]
        private readonly IHostingEnvironment hostingEnvironment;

        [Obsolete]
        public DepartmentController(IDepartmendServices departservices,
            UserManager<ApplicationUser> usermanger, IDoctorservices doctorservices)
        {
            this.departservices = departservices;
            this.usermanger = usermanger;
            this.doctorservices = doctorservices;
           
          

        }



        [HttpGet("getalldepartments")]
        public async Task<IActionResult> Getalldepartments()
        {
            var departments = await departservices.listofDepartment();
            if (departments == null)
            {
                return BadRequest();
            }

            return Ok(departments);
        }
        [HttpGet("getallhospital")]

        public async Task<IActionResult> getallhospital()
        {

            var hospitals = await departservices.Listofhospitals();
            if (hospitals == null)
            {
                return Ok("no hospitals");
            }

            return Ok(hospitals);
        }
        [HttpGet("getalldoctors")]

        public async Task<IActionResult> Getalldoctors()
        {

            var doctors = await departservices.listofdoctors();
            if (doctors == null)
            {
                return BadRequest();
            }

            return Ok(doctors);
        }
        [HttpGet("getalldoctorindepartment")]
        public async Task<IActionResult> getalldoctorindepartment(int id)
        {
            var department = await departservices.listofdoctorindepartment(id);


            if (department == null)
            {
                return Ok("no department Here");
            }
            return Ok(department);

        }

        [HttpPost("adddepart")]
        public IActionResult postdepartment( string name,  string hospitalid)
        {
            var adddepart = departservices.addDepartment(name, hospitalid);
            return Ok();
        }
        [HttpDelete("deletedepart")]
        public IActionResult deletedepartment(int id)
        {
             departservices.deletedepartment(id);

            return Ok();
        }
        [HttpGet("listallbeds")]
        public async Task<IActionResult> listbeds()
        {
            var beds =await departservices.listallBeds();
            
            return Ok(beds);
        }
        [HttpGet("listavailablebeds")]
        public async Task<IActionResult> listavailablebeds()
        {
            var beds = await departservices.listavailableBeds();

            return Ok(beds);
        }
        [HttpGet("listbookedbeds")]
        public async Task<IActionResult> listbookedbeds()
        {
            var beds = await departservices.listBookedbeds();

            return Ok(beds);
        }
        [HttpPost("bookingbed")]
        public async Task<IActionResult> bookingbed( string patientid,  string bedid)
        {
            var booking = await departservices.bookingbed(patientid,bedid);

            return Ok(booking);
        }
        [HttpPost("bookingdoctor")]
        public async Task<IActionResult> bookingdoctor(string id, string patientId)
        {
           
            var booking = await doctorservices.BookingDoctor(id,patientId);

            return Ok(booking);
        }
        [HttpGet("getidlogeduser")]
        public IActionResult getidlogeduser()
        {

            var booking = doctorservices.GetUserid();

            return Ok(booking);
        }
        [HttpGet("listyourdoctors")]
        public async Task<IActionResult> listyourdoctors(string PatientId)
        {

            var booking = await doctorservices.Doctorsyou(PatientId);

            return Ok(booking);
        }

        [HttpGet("listyourPatient")]
        public async Task<IActionResult> listyourPatient(string doctorId)
        {

            var booking = await doctorservices.patientsdoctor(doctorId);

            return Ok(booking);
        }

    [HttpPost("checkoutbed")]
        public async Task<IActionResult> checkoutbed(string patientid,string bedid)
        {
            var booking = await departservices.Cancellation(patientid, bedid);

            return Ok(booking);
        }
        [HttpGet("departinhospital")]
        public  async Task<IActionResult> departinhospital(string id)
        {
            var list = await departservices.departinhospital(id);

            if (list == null)
            {
                return BadRequest();
            }

            return Ok(list);
        }

        [HttpGet("GetDoctorById")]
        public  IActionResult getDoctorById(string id)
        {
            var doctor =  departservices.returndoctor(id);

            if (doctor == null)
            {
                return BadRequest();
            }

            return Ok(doctor);
        }


        [HttpGet("GetPatientById")]
        public IActionResult GetPatientById(string id)
        {
            var doctor = departservices.returPatient(id);

            if (doctor == null)
            {
                return BadRequest();
            }

            return Ok(doctor);
        }





    }
}
