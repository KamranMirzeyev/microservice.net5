using ASP.NET_Web.Models.FakePayments;
using ASP.NET_Web.Services.Interfaces;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace ASP.NET_Web.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly HttpClient _httpClient;

        public PaymentService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> ReceivePayment(PaymentInfoInput paymentInfoInput)
        {
            var response = await _httpClient.PostAsJsonAsync<PaymentInfoInput>("fakepayments", paymentInfoInput);

            return response.IsSuccessStatusCode;
        }
    }
}
