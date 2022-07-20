using HospitalManegement.IServices;
using HospitalManegement.IuserServices;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Modelsidentiy.Shared;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace HospitalManegement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IuserServicer iuserServicer;
        private readonly IDoctorservices idoctorservices;

        public AuthController(IuserServicer iuserServicer, IDoctorservices idoctorservices)
        {
            this.iuserServicer = iuserServicer;
            this.idoctorservices = idoctorservices;
        }
        //[HttpGet("google")]
        //public IActionResult GooGleLogin()
        //{
        //    var propiretice = new AuthenticationProperties { RedirectUri = Url.Action("GoogleResponse") };
        //    return  Challenge(propiretice, GoogleDefaults.AuthenticationScheme);


        //}
        //[Route("googleresponse")]
        //public async Task<IActionResult> Googleresponse()
        //{
        //    var result= await HttpContext.AuthenticateAsync()
        //}






        [HttpPost("Register")]
        public async Task<IActionResult> registerasync([FromBody] RegisterViewmodel model)
        {
            //if (ModelState.IsValid)
            //{

                var result = await iuserServicer.Regesteruserasync(model);
                if (result.IsAuthenticated)
                {
                    return Ok(result);
                    //return ok(new {token =result.token ,expiration =result.expireon});
                }
                else
                {
                    return Ok(result);
                }

            //}
            //return Ok("some proberty not valid");

        }



        [HttpPost("RegisterDoctor")]
        public async Task<IActionResult> registerdoctorasync([FromBody] RegisterDoctorviewModel model)
        {
            
            if (ModelState.IsValid)
            {
                var result = await idoctorservices.RegesterDoctor(model);

                if (result.IsAuthenticated)
                {
                    return Ok(result);
                    //return ok(new {token =result.token ,expiration =result.expireon});
                }
                else
                {
                    return Ok(result);
                }

            }
          var errors =  new authmodel()
            {Message= "some proberty not valid",
          IsAuthenticated=false,
          Token="",
          };
            return Ok(errors);

        }

        [HttpPost("LoginDoctor")]
        public async Task<IActionResult> LoginDoctorasync([FromBody] LoginViewModel model)
        {

            //if (!ModelState.IsValid)
            //    return BadRequest(ModelState);

            var result = await idoctorservices.LoginDoctor(model);


            if (!result.IsAuthenticated)
                return Ok(result);

            return Ok(result);

        }


        [HttpPost("Login")]
        public async Task<IActionResult> Loginasync([FromBody] LoginViewModel model)
        {

            //if (!ModelState.IsValid)
            //    return BadRequest(ModelState);

            var result = await iuserServicer.Loginuser(model);

            if (!result.IsAuthenticated)
                return Ok(result);

            return Ok(result);

        }




    }
}
