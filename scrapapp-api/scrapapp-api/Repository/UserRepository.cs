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
        public async Task<int> SaveRefreshToken(int userId, string token, string deviceId)
        {

            var parameters = new DynamicParameters();
            parameters.Add("@UserId", userId);
            parameters.Add("@Token", token);
            parameters.Add("@DeviceId", deviceId);
            parameters.Add("@UserAgent", HttpContext.Current.Request.UserAgent);
            parameters.Add("@IpAddress", HttpContext.Current.Request.UserHostAddress);
            parameters.Add("@ExpiryDate", DateTime.Now.AddDays(7));
            parameters.Add("@IsRevoked", false);
            parameters.Add("@CreatedAt", DateTime.Now);
            string query = @"INSERT INTO RefreshTokens 
                (UserId,Token,DeviceId,UserAgent,IpAddress,ExpiryDate,IsRevoked,CreatedAt)
 values
 (@UserId,@Token,@DeviceId,@UserAgent,@IpAddress,@ExpiryDate,@IsRevoked,@CreatedAt)
";

          await _dataService.ExecuteAsync(query,parameters);
            return 1;
        }
        public async Task<IEnumerable<Mst_City>> GetCity()
        {
            var parameters = new DynamicParameters();



            return await _dataService.GetAllAsync<Mst_City>(
                "SELECT * FROM [Mst_City]",
                parameters
            );


        }


        public async Task<IEnumerable<RefreshTokens>> GetRefreshToken(string Token)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@Token", Token);


            return await _dataService.GetAllAsync<RefreshTokens>(
                "SELECT top 1 * FROM RefreshTokens where Token=@Token",
                parameters
            );


        }


        public async void RevokedToken(string Token)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@Token", Token);


            await _dataService.ExecuteAsync(
                "delete from RefreshTokens where Token=@Token",
                parameters
            );


        }
    }
}