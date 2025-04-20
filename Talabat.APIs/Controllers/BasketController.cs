using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.APIs.Errors;
using Talabat.Core.Entities;
using Talabat.Core.Repository.Core;

namespace Talabat.APIs.Controllers
{
    public class BasketController : BaseController
    {
        private readonly IBasketRepository _basketRepo;

        public BasketController(IBasketRepository basketRepo) 
        {
            _basketRepo = basketRepo;
        }
        [HttpGet("{BasketId}")]
        public async Task<ActionResult<CustomerBasket>> GetCustomerBasket(string BasketId)
        {
            var Basket = await _basketRepo.GetBasketAsync(BasketId); 
            return Basket is null ? new CustomerBasket(BasketId) : Ok(Basket);
        }
        [HttpPost]
        public async Task<ActionResult<CustomerBasket>> UpdateBasket(CustomerBasket Basket)
        {
            var CreatedOrUpdatedBasket = await _basketRepo.UpdateBasketAsync(Basket);
            return CreatedOrUpdatedBasket is null ? BadRequest(new ApiResponse(400)) :Ok(Basket);
        }
        [HttpDelete]
        public async Task<ActionResult<bool>> DeleteBasket(string BasketId)
        {
            return await _basketRepo.DeleteBasketAsync(BasketId);

        }
    }
}
