using AutoMapper;
using Microsoft.Extensions.Configuration;
using Talabat.APIs.DTO;
using Talabat.Core.Entities;
using Talabat.Core.OrderAggregate;

namespace Talabat.APIs.Helpers
{
    public class OrderToReturnPictureUrlResolver : IValueResolver<OrderItem, OrderItemDto, string>
    {
        private readonly IConfiguration _configuration;
        public OrderToReturnPictureUrlResolver(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string Resolve(OrderItem source, OrderItemDto destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.product.PictureURL))
                return $"{_configuration["ApiBaseUrl"]}/{source.product.PictureURL}";

            return string.Empty;
        }
    }
}
