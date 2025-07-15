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
        [ProducesResponseType(typeof(IReadOnlyList<Order>), statusCode: 200)]
        [ProducesResponseType(typeof(ApiResponse), statusCode: 404)]
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<Order>>> GetOrdersForCurrentUser()
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Email);
            var Orders = await _orderService.GetOrdersForSpecificUserAsync(userEmail);
            if (Orders is null) return NotFound(new ApiResponse(404, "No Orders have been made for this user"));
            return Ok(Orders);
        }
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<Order>> GetOrderById(int OrderId)
        {
            var BuyerEmail = User.FindFirstValue(ClaimTypes.Email);
            var order = await _orderService.GetOrdersByIdForSpecificUserAsync(BuyerEmail, OrderId);
            if (order is null) return NotFound(new ApiResponse(404, $"No Order Found for this id {OrderId}"));
            return Ok(order);
        }
    }
}
