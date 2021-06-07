using Basket.Dtos;
using Shared.DTO;
using System.Threading.Tasks;

namespace Basket.Service
{
    public interface IBasketService
    {
        Task<Responce<BasketDto>> GetBasket(string userId);
        Task<Responce<bool>> SaveOrUpdate(BasketDto basketDto);
        Task<Responce<bool>> Delete(string userId);
    }
}
