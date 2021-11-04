using Shared.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Discount.Services
{
    public interface IDiscountService
    {
        Task<Responce<List<Models.Discount>>> GetAll();
        Task<Responce<Models.Discount>> GetById(int id);
        Task<Responce<NoContent>> Save(Models.Discount discount);
        Task<Responce<NoContent>> Update(Models.Discount discount);
        Task<Responce<NoContent>> Delete(int id);
        Task<Responce<Models.Discount>> GetCodeAndUserID(string code,string userId);
    }
}
