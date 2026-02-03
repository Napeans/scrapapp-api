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
    public class ProductRepository
    {
        private readonly DapperDataService _dataService;
        public ProductRepository()
        {
            _dataService = new DapperDataService();
        }

        public async Task<IEnumerable<Mst_Scrap_Type>> GetScrapType()
        {
            var parameters = new DynamicParameters();



            return await _dataService.GetAllAsync<Mst_Scrap_Type>(
                "SELECT * FROM [Mst_Scrap_Type]",
                parameters
            );


        }


        public async Task<IEnumerable<ProductDetailsModel>> GetProductDetails(int CityId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@CityId", CityId);

            string query = @"SELECT
    a.ProductId,
    a.ScrapTypeId,
    a.ProductName,
    a.ProductImage,
    a.OrderBy,
    b.MarketPrice,
    b.OurPrice,
    b.QuantityPerPrice
    --b.RevisedDate
FROM Mst_Products a
INNER JOIN
(
    SELECT *,
           ROW_NUMBER() OVER (
               PARTITION BY ProductId
               ORDER BY 
                   ISNULL(RevisedDate, '19000101') DESC,
                   ProductPriceId DESC
           ) AS rn
    FROM Mst_Product_Price
    WHERE CityId = @CityId
) b
ON a.ProductId = b.ProductId
WHERE b.rn = 1;
";

            return await _dataService.GetAllAsync<ProductDetailsModel>(query, parameters);


        }
    }
}