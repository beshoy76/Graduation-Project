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
    public class medicineController : Controller
    {
        private readonly IDepartmendServices departservices;
        private readonly UserManager<ApplicationUser> usermanger;
        private readonly IDoctorservices doctorservices;

        public object medicines { get; private set; }

        public medicineController(IDepartmendServices departservices, UserManager<ApplicationUser> usermanger, IDoctorservices doctorservices)
        {
            this.departservices = departservices;
            this.usermanger = usermanger;
            this.doctorservices = doctorservices;
        }


        [HttpGet("getallmedicines")]
        public async Task<IActionResult> getallmedicines()
        {
            var list = await departservices.listofmedicines();

            if (list == null)
            {
                return Ok("no medicines available");
            }

            return Ok(list);
        }

        [HttpGet("getallMedicinesByName")]
        public async Task<IActionResult> getallMedicinesByName(string name)
        {
            var medicines = await departservices.listofmedicinesByName(name);


            if (medicines == null)
            {
                return Ok("no medicines available");
            }
            return Ok(medicines);

        }

        [HttpGet("getmedicinesById")]
        public async Task<IActionResult> listofmedicinesById(string id)
        {
            var medicines = await departservices.listofmedicinesById(id);


            if (medicines == null)
            {
                return Ok("no medicines available");
            }
            return Ok(medicines);

        }

        [HttpDelete("deletemedicineById")]
        public IActionResult deleteMedicineById(int id)
        {
            departservices.deleteMedicineByID(id);

            return Ok();

        }

        [HttpPost("addmedicine")]
        public IActionResult postmedicine(string patientId,  String medicineName, string phone, string quantity, string ExprDate)
        {
            var medicine = departservices.addmedicine(patientId, medicineName,  phone,quantity, ExprDate);
            return Ok();
        }

        [HttpPut("editmedicine")]
        //string patientId, String medicineName, string phone, string quantity, String exprDate
        public async Task<IActionResult> editmedicine(int id, string patientId, String medicineName, string phone, string quantity, String exprDate)
        {
            //if (id != medicine.MedicineId)
            //{
            //    return BadRequest();

            //}

            //, patientId, medicineName, phone, quantity,exprDate
             var medicines = await departservices.editmedicine(id, patientId, medicineName, phone, quantity, exprDate);

            //medicines
            return Ok();
        }




    }





}
