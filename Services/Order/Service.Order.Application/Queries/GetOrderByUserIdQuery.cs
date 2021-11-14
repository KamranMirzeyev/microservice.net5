using MediatR;
using Service.Order.Application.Dtos;
using Shared.DTO;
using System.Collections.Generic;

namespace Service.Order.Application.Queries
{
    public class GetOrderByUserIdQuery :IRequest<Responce<List<OrderDto>>>
    {
        public string UserId { get; set; }
    }
}
