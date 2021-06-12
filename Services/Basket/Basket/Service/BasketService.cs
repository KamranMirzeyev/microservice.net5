using Basket.Dtos;
using Shared.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace Basket.Service
{
    public class BasketService : IBasketService
    {
        private readonly RedisService _redisService;

        public BasketService(RedisService redisService)
        {
            _redisService = redisService;
        }

        public async Task<Responce<bool>> Delete(string userId)
        {
            var status = await _redisService.GetDb().KeyDeleteAsync(userId);
            return status ? Responce<bool>.Success(200) : Responce<bool>.Fail("Basket not found", 404); 
        }

        public async Task<Responce<BasketDto>> GetBasket(string userId)
        {
            var exsit = await _redisService.GetDb().StringGetAsync(userId);
            if (string.IsNullOrEmpty(exsit))
            {
                return Responce<BasketDto>.Fail("Basket not found", 404);
            }
            return Responce<BasketDto>.Success(JsonSerializer.Deserialize<BasketDto>(exsit), 200);
        }

        public async Task<Responce<bool>> SaveOrUpdate(BasketDto basketDto)
        {
            var status = await _redisService.GetDb().StringSetAsync(basketDto.UserId, JsonSerializer.Serialize(basketDto));
            return status ? Responce<bool>.Success(200) : Responce<bool>.Fail("Basket UserId not update or save",500);
        }
    }
}
