using System;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Rawpotion.Identity.Controllers
{
    [Route("api/session")]
    public class SessionController : Controller
    {
        public class SetTokenRequest
        {
            public string Token { get; set; }
        }
        
        [HttpPost]
        public IActionResult SetToken([FromBody]SetTokenRequest tokenRequest)
        {
            HttpContext.Session.Set("rawpotion.user.token", Convert.FromBase64String(tokenRequest.Token));
            return Ok();
        }

        [HttpGet]
        public IActionResult GetToken()
        {
            var rawToken = HttpContext.Session.Get("rawpotion.user.token");
            return Ok(new
            {
                Token = Convert.ToBase64String(rawToken)
            });
        }
    }
}