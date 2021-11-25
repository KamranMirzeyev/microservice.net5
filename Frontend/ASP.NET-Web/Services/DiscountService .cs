using ASP.NET_Web.Models.Discounts;
using ASP.NET_Web.Services.Interfaces;
using Shared.DTO;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace ASP.NET_Web.Services
{
    public class DiscountService : IDiscountService
    {
        private readonly HttpClient _httpClient;

        public DiscountService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<DiscountViewModel> GetDiscount(string discountCode)
        {
            //[controller]/[action]/{code}

            var response = await _httpClient.GetAsync($"discounts/GetByCode/{discountCode}");

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var discount = await response.Content.ReadFromJsonAsync<Responce<DiscountViewModel>>();

            return discount.Data;
        }
    }
}
