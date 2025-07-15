using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;
using System.Security.Claims;
using Talabat.APIs.DTO;
using Talabat.APIs.Errors;
using Talabat.Core;
using Talabat.Core.OrderAggregate;
using Talabat.Core.Services.Core;
using Talabat.Service;
using Order = Talabat.Core.OrderAggregate.Order;

namespace Talabat.APIs.Controllers
{
    public class OrdersController :BaseController
    {
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public OrdersController(IOrderService orderService, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _orderService = orderService;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
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
            var MappedOrders = _mapper.Map<IReadOnlyList<Order>, IReadOnlyList<OrderToReturnDto>>(Orders);
            return Ok(MappedOrders);
        }
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<Order>> GetOrderById(int OrderId)
        {
            var BuyerEmail = User.FindFirstValue(ClaimTypes.Email);
            var order = await _orderService.GetOrdersByIdForSpecificUserAsync(BuyerEmail, OrderId);
            if (order is null) return NotFound(new ApiResponse(404, $"No Order Found for this id {OrderId}"));
            var MappedOrder = _mapper.Map<Order, OrderToReturnDto>(order);
            return Ok(MappedOrder);
        }
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<DeliveryMethod>>> GetAllDeliveryMethod()
        {
            var DeliveryMethods = await _unitOfWork.Repository<DeliveryMethod>().GetAllAsync();
            return Ok(DeliveryMethods);
        }
    }
}
