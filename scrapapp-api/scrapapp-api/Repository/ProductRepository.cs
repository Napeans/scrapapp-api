using Dapper;
using scrapapp_api.Common;
using scrapapp_api.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace scrapapp_api.Repository
{
    public class ProductRepository
    {
        private readonly DapperDataService _dataService;
        public ProductRepository()
        {
            _dataService = new DapperDataService();
        }

        public async Task<List<Mst_Scrap_Type>> GetScrapType()
        {
            var parameters = new DynamicParameters();



            var list= await _dataService.GetAllAsync<Mst_Scrap_Type>(
                "SELECT * FROM [Mst_Scrap_Type]",
                parameters
            );

            var listData = list.ToList();

            listData.Insert(0, new Mst_Scrap_Type
            {
                ScrapTypeId = 0,
                ScrapType = "All"
            });
            return listData;
        }

        public async Task<(List<Mst_Scrap_Type>, List<ProductDetailsModel>)> GetProductDetails(int CityId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@CityID", CityId);
            using (var multi = await _dataService.QueryMultipleAsync(
                "GETProductList",
                parameters,
                CommandType.StoredProcedure))
            {
                var scrapTypes = (await multi.ReadAsync<Mst_Scrap_Type>()).AsList();
                var scrapCategories = (await multi.ReadAsync<ProductDetailsModel>()).AsList();
                return (scrapTypes, scrapCategories);
            }
        }
    }
}