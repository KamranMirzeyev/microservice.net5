using ASP.NET_Web.Models.Discounts;
using System.Threading.Tasks;

namespace ASP.NET_Web.Services.Interfaces
{
    public interface IDiscountService
    {
        Task<DiscountViewModel> GetDiscount(string discountCode);
    }
}
