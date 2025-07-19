using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.APIs.DTO;
using Talabat.APIs.Errors;
using Talabat.Core.Entities;
using Talabat.Core.Services.Core;

namespace Talabat.APIs.Controllers
{
    public class PaymentController : BaseController
    {
        private readonly IPaymentService _paymentService;
        private readonly IMapper _mapper;

        public PaymentController(IPaymentService paymentService, IMapper mapper)
        {
            _paymentService = paymentService;
            _mapper = mapper;
        }
        [HttpPost]
        public async Task<ActionResult<CustomerBasketDto>> CreateOrUpdatePaymentIntent(string BasketId)
        {
            var Basket =  await _paymentService.CreateOrUpdatePaymentIntentAsync(BasketId);
            if (Basket is null) return BadRequest(new ApiResponse(404, "There is no Basket with this key "));
            return Ok(Basket);
        }
    }
}
