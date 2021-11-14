using MediatR;
using Microsoft.AspNetCore.Mvc;
using Service.Order.Application.Commands;
using Service.Order.Application.Queries;
using Shared.ControllerBases;
using Shared.Service;
using System.Threading.Tasks;

namespace Service.Order.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : CustomControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ISharedIdentityService _service;

        public OrderController(IMediator mediator, ISharedIdentityService service)
        {
            _mediator = mediator;
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var responce = await _mediator.Send(new GetOrderByUserIdQuery { UserId = _service.GetUserId });
            return CreateActionResultInttance(responce);
        }

        [HttpPost]
        public async Task<IActionResult> Post(CreateOrderCommand command)
        {
            var responce = await _mediator.Send(command);
            return CreateActionResultInttance(responce);
        }

    }
}
