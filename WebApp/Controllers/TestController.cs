using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AspNet.Security.OpenIdConnect.Extensions;
using AspNet.Security.OpenIdConnect.Primitives;
using Microsoft.AspNetCore.Authorization;

namespace WebApp.Controllers
{
    //[Produces("application/json")]
    //[Route("api/Test")]
    public class TestController : Controller
    {
        [Authorize, HttpGet("api/Test")]
        public IActionResult TestMessage()
        {
            try
            {
                return Json(
                    new
                    {
                        Subject = User.GetClaim(OpenIdConnectConstants.Claims.Subject),
                        Name = User.Identity.Name
                    }
                );
            }
            catch (Exception Ex)
            {
                return null;
            }
        }

        [HttpGet("~/api/Test2")]
        public IActionResult TestMessage2()
        {
            return Json(
                 new
                 {
                     Subject = User.GetClaim(OpenIdConnectConstants.Claims.Subject),
                     Name = User.Identity.Name,
                     Hola = "Hola Mundo"
                 }
             );
        }
    }
}