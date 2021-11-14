using Microsoft.AspNetCore.Mvc;
using Shared.ControllerBases;
using Shared.DTO;

namespace Service.FakePayment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : CustomControllerBase
    {
        [HttpPost]
        public IActionResult Post()
        {
            return CreateActionResultInttance(Responce<NoContent>.Success(200));
        }
    }
}
