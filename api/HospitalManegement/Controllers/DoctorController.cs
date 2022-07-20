using HospitalManegement.IServices;
using HospitalManegement.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace HospitalManegement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Doctor")]
    public class DoctorController : ControllerBase
    {
        private readonly IDoctorservices doctorservice;

        public DoctorController(IDoctorservices doctorservice)
        {
            this.doctorservice = doctorservice;
        }


       

        [HttpGet("getdoctorInfo")]
        public async Task<IActionResult> doctorInfor()
        {
            var doctor = await doctorservice.doctorInfo();
            return Ok(doctor);

        }








    }
}
