using AutoMapper;
using Service.Order.Application.Dtos;

namespace Service.Order.Application.Mapping
{
    internal class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<Domain.OrderAggregate.Order, OrderDto>().ReverseMap();
            CreateMap<Domain.OrderAggregate.OrderItem, OrderItemDto>().ReverseMap();
            CreateMap<Domain.OrderAggregate.Address, AddressDto>().ReverseMap();
           
        }
    }
}
