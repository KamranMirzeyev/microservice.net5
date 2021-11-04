using Dapper;
using Microsoft.Extensions.Configuration;
using Npgsql;
using Shared.DTO;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Discount.Services
{
    public class DiscountService : IDiscountService
    {
        private readonly IConfiguration _configuration;
        private readonly IDbConnection _dbConnection;

        public DiscountService(IConfiguration configuration)
        {
            _configuration = configuration;

            _dbConnection = new NpgsqlConnection(_configuration.GetConnectionString("DefaultConnection"));
        }
        public async Task<Responce<NoContent>> Delete(int id)
        {
            var status = await _dbConnection.ExecuteAsync("delete from discount where id=@Id", new { Id = id });
            return status > 0 ? Responce<NoContent>.Success(204) : Responce<NoContent>.Fail("not found", 404);
        }

        public async Task<Responce<List<Models.Discount>>> GetAll()
        {
            var data = await _dbConnection.QueryAsync<Models.Discount>("select * from discount");
            return Responce<List<Models.Discount>>.Success(data.ToList(),200);
        }

        public async Task<Responce<Models.Discount>> GetById(int id)
        {
            var discount = (await _dbConnection.QueryAsync<Models.Discount>("select * from discount where id=@Id", new { Id = id })).SingleOrDefault();

            if (discount == null)
            {
                return Responce<Models.Discount>.Fail("Not found", 404);
            }
            return Responce<Models.Discount>.Success(discount, 200);
        }

        public async Task<Responce<Models.Discount>> GetCodeAndUserID(string code, string userId)
        {
            var discount = await _dbConnection.QueryAsync<Models.Discount>("select * from discount where code=@code and userid=@UserId", new { Code = code, UserId = userId });
            var hasDiscount = discount.FirstOrDefault();
            return hasDiscount == null ? Responce<Models.Discount>.Fail("not fount", 404): Responce<Models.Discount>.Success(hasDiscount,200);
        }

        public async Task<Responce<NoContent>> Save(Models.Discount discount)
        {
            var status = await _dbConnection.ExecuteAsync("INSERT INTO discount (userid,rate,code) VALUES (@UserId,@Rate,@Code)", discount);
            if (status>0)
            {
                return Responce<NoContent>.Success(204);
            }
            return Responce<NoContent>.Fail("error db connection", 500);
        }

        public async Task<Responce<NoContent>> Update(Models.Discount discount)
        {
            var status = await _dbConnection.ExecuteAsync("update discount set userid=@UserId,code=@Code,rate=@Rate where id=@Id", discount);

            if (status > 0)
            {
                return Responce<NoContent>.Success(204);
            }
            return Responce<NoContent>.Fail("not found", 404);
        }
    }
}
