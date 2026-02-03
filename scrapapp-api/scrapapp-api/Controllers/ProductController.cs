using scrapapp_api.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace scrapapp_api.Controllers
{
    [RoutePrefix("api/product")]
    public class ProductController : ApiController
    {
        private readonly ProductRepository _dal;
        public ProductController() {
            _dal = new ProductRepository();
        }
        [HttpGet]
        [Route("GetScrapType")]
        public async Task<IHttpActionResult> GetScrapType()
        {
            var data = await _dal.GetScrapType();
            return Ok(data);
        }

        [HttpGet]
        [Route("GetProductDetails/{CityId:int}")]
        public async Task<IHttpActionResult> GetProductDetails(int CityId)
        {
            var data = await _dal.GetProductDetails(CityId);
            return Ok(data);
        }
    }
}
