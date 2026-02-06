using scrapapp_api.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Http;

namespace scrapapp_api.Controllers
{
    [Authorize]
    [RoutePrefix("api/user")]
    public class UserController : ApiController
    {

        private readonly UserRepository _dal;
        public UserController()
        {
            _dal = new UserRepository();
        }
        [HttpGet]
        [Route("GetCity")]
        public async Task<IHttpActionResult> GetCity()
        {
            var identity = (ClaimsIdentity)User.Identity;

            var userId = identity.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var data = await _dal.GetCity();
            return Ok(data);
        }


    }
}
