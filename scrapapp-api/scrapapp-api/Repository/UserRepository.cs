using Dapper;
using scrapapp_api.Common;
using scrapapp_api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace scrapapp_api.Repository
{
    public class UserRepository
    {
        private readonly DapperDataService _dataService;
        public UserRepository()
        {
            _dataService = new DapperDataService();
        }

        public async Task<IEnumerable<Mst_City>> GetCity()
        {
            var parameters = new DynamicParameters();



            return await _dataService.GetAllAsync<Mst_City>(
                "SELECT * FROM [Mst_City]",
                parameters
            );


        }
    }
}