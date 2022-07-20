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
using HospitalManegement.IuserServices;

namespace HospitalManegement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "User")]
    public class patiebtInfoController : ControllerBase
    {
        private readonly IDoctorservices doctorservice;

        public patiebtInfoController(IDoctorservices doctorservice)
        {
            this.doctorservice = doctorservice;
        }




        [HttpGet("getpatientInfo")]
        public async Task<IActionResult> patientInfo()
        {
            var patient = await doctorservice.patientInfo();

            return Ok(patient);

        }





    }
}
