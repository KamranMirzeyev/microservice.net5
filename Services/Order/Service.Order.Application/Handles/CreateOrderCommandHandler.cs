using MediatR;
using Service.Order.Application.Commands;
using Service.Order.Application.Dtos;
using Service.Order.Domain.OrderAggregate;
using Service.Order.Infrastructure;
using Shared.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Service.Order.Application.Handles
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, Responce<CreatedOrderDto>>
    {
        private readonly OrderDbContext _context;

        public CreateOrderCommandHandler(OrderDbContext context)
        {
            _context = context;
        }

        public async Task<Responce<CreatedOrderDto>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var address = new Address(request.Address.Province, request.Address.District, request.Address.Street, request.Address.ZipCode, request.Address.Line);

            Domain.OrderAggregate.Order order = new Domain.OrderAggregate.Order(request.BuyerId,address);

            request.OrderItems.ForEach(item =>
            {
                order.AddOrderItem(item.ProductId, item.ProductName, item.Price, item.PictureUrl);
            });

            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();
            return Responce<CreatedOrderDto>.Success(new CreatedOrderDto() { OrderId = order.Id },200);
        }
    }
}
