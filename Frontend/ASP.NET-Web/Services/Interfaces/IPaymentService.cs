using ASP.NET_Web.Models.FakePayments;
using System.Threading.Tasks;

namespace ASP.NET_Web.Services.Interfaces
{
    public interface IPaymentService
    {
        Task<bool> ReceivePayment(PaymentInfoInput paymentInfoInput);
    }
}
