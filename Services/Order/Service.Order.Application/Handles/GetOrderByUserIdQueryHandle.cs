using MediatR;
using Microsoft.EntityFrameworkCore;
using Service.Order.Application.Dtos;
using Service.Order.Application.Mapping;
using Service.Order.Application.Queries;
using Service.Order.Infrastructure;
using Shared.DTO;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Service.Order.Application.Handles
{
    public class GetOrderByUserIdQueryHandle : IRequestHandler<GetOrderByUserIdQuery, Responce<List<OrderDto>>>
    {

        private readonly OrderDbContext _context;
       
        public GetOrderByUserIdQueryHandle(OrderDbContext context)
        {
            _context = context;
        }

        public async Task<Responce<List<OrderDto>>> Handle(GetOrderByUserIdQuery request, CancellationToken cancellationToken)
        {
            var order = await _context.Orders.Include(x=>x.OrderItems).Where(x=>x.BuyerId == request.UserId).ToListAsync();

            if (!order.Any())
            {
                return Responce<List<OrderDto>>.Success(new List<OrderDto>(), 200);
            }
            var dtos = ObjectMapping.Mapper.Map<List<OrderDto>>(order);
            return Responce<List<OrderDto>>.Success(dtos,200);
        }
    }
}
