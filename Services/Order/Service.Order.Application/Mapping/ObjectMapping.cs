using AutoMapper;
using System;

namespace Service.Order.Application.Mapping
{
    public static class ObjectMapping
    {
        private static readonly Lazy<IMapper> lazy = new Lazy<IMapper>(() =>
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<AutoMapping>();
            });

            return config.CreateMapper();
        });

        public static IMapper Mapper => lazy.Value;
    }; 

}