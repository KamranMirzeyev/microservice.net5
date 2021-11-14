using MediatR;
using Service.Order.Application.Dtos;
using Shared.DTO;
using System.Collections.Generic;

namespace Service.Order.Application.Commands
{
    public class CreateOrderCommand : IRequest<Responce<CreatedOrderDto>>
    {
        public string BuyerId { get; set; }
        public List<OrderItemDto> OrderItems { get; set; }
        public AddressDto Address { get; set; }
    }
}
