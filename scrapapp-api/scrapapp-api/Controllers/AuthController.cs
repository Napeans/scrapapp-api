using scrapapp_api.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace scrapapp_api.Controllers
{
    [RoutePrefix("api/auth")]
    public class AuthController : ApiController
    {
        [HttpPost]
        [Route("login")]
        public IHttpActionResult Login(LoginModel model)
        {
            // Example validation (replace with DB check)
            if (model.Username == "admin" && model.Password == "1234")
            {
                int userId = 101; // from DB
                var token = JwtTokenHelper.GenerateToken(userId, model.Username);

                return Ok(new
                {
                    token,
                    userId // optional: return separately
                });
            }

            return Unauthorized();
        }
    }

    public class LoginModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
