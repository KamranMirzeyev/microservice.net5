﻿using MassTransit;
using Microsoft.EntityFrameworkCore;
using Service.Order.Infrastructure;
using Shared.Messages;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Order.Application.Consumers
{
    public class CourseNameChangedEventConsumer : IConsumer<CourseNameChangedEvent>
    {
        private readonly OrderDbContext _orderDbContext;

        public CourseNameChangedEventConsumer(OrderDbContext orderDbContext)
        {
            _orderDbContext = orderDbContext;
        }
        public async Task Consume(ConsumeContext<CourseNameChangedEvent> context)
        {
            var orderItems = await _orderDbContext.OrderItems.Where(x => x.ProductId == context.Message.CourseId).ToListAsync();

            orderItems.ForEach(x =>
            {
                x.UpdateOrderItem(context.Message.UpdateName, x.PictureUrl, x.Price);
            });

            await _orderDbContext.SaveChangesAsync();
        }
    }
}
