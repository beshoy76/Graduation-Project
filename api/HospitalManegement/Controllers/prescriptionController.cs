using HospitalManegement.IServices;
using HospitalManegement.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Modelsidentiy.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
namespace HospitalManegement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class prescriptionController : Controller
    {

        private readonly IDepartmendServices departservices;
        private readonly UserManager<ApplicationUser> usermanger;
        private readonly IDoctorservices doctorservices;
        public prescriptionController(IDepartmendServices departservices, UserManager<ApplicationUser> usermanger, IDoctorservices doctorservices)
        {
            this.departservices = departservices;
            this.usermanger = usermanger;
            this.doctorservices = doctorservices;
        }


        [HttpGet("getallprescriptions")]
        public async Task<IActionResult> getallprescriptions(string patientid)
        {
            var list = await departservices.listofprescrption(patientid);

            if (list == null)
            {
                return Ok("no prescriptions available");
            }

            return Ok(list);
        }

        [HttpGet("getprescriptionById")]
        public async Task<IActionResult> listofprescrptionById(int id)
        {
            var prescription = await departservices.listofprescrptionById(id);


            if (prescription == null)
            {
                return Ok("no medicines available");
            }
            return Ok(prescription);

        }

        [HttpDelete("deleteprescriptionsById")]
        public IActionResult deleteprescriptionsById(int id)
        {
            departservices.deleteprescrptionByID(id);

            return Ok();

        }

        [HttpPost("addPriscription")]
        public IActionResult addPriscription(string datetime, string medicineName, string notes, string patientId, string department)
        {
            var pre = departservices.addPriscription(datetime, medicineName, notes, patientId, department);
            return Ok();
        }

        [HttpPut("editpriscription")]
       
        public async Task<IActionResult> editpriscription(int id, string patientId, string medicineName, string notes, string datetime, string department)
        {
           
            var priscription = await departservices.editpriscription(id, patientId, medicineName, notes, datetime,  department);

           
            return Ok();
        }



    }
}
