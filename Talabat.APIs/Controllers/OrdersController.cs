using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Talabat.APIs.DTO;
using Talabat.APIs.Errors;
using Talabat.Core.OrderAggregate;
using Talabat.Core.Services.Core;
using Talabat.Service;

namespace Talabat.APIs.Controllers
{
    public class OrdersController :BaseController
    {
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;

        public OrdersController(IOrderService orderService, IMapper mapper)
        {
            _orderService = orderService;
            _mapper = mapper;
        }
        [ProducesResponseType(typeof(Order),statusCode:200)]
        [ProducesResponseType(typeof(ApiResponse), statusCode:400)]
        [HttpPost]
        [Authorize]
       public async Task<ActionResult<Order>> CreateOrder(OrderDto orderDto)
       {
            var BuyerEmail = User.FindFirstValue(ClaimTypes.Email);
            var MappedAddress = _mapper.Map<AddressDto, Address>(orderDto.ShippingAddress);
            var Result = await _orderService.CreateOrderAsync(BuyerEmail, orderDto.BasketId, orderDto.DeliveryMethodId, MappedAddress);
            if (Result is null) return BadRequest(new ApiResponse(400, "There is a problem with your order "));
            return Ok(Result);
       }
    }
}
