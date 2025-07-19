using Microsoft.AspNetCore.Http.Connections;
using Microsoft.Extensions.Configuration;
using Stripe;
using Stripe.V2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core;
using Talabat.Core.Entities;
using Talabat.Core.OrderAggregate;
using Talabat.Core.Repository.Core;
using Talabat.Core.Services.Core;

namespace Talabat.Service
{
    public class PaymentService : IPaymentService
    {
        private readonly IConfiguration _configuration;
        private readonly IBasketRepository _basketRepository;
        private readonly IUnitOfWork _unitOfWork;

        public PaymentService(IConfiguration configuration, IBasketRepository basketRepository,IUnitOfWork unitOfWork)
        {
            this._configuration = configuration;
            this._basketRepository = basketRepository;
            this._unitOfWork = unitOfWork;
        }
        public async Task<CustomerBasket?> CreateOrUpdatePaymentIntentAsync(string BasketId)
        {
            StripeConfiguration.ApiKey = _configuration["StripeSettings:Secretkey"];
            var Basket = await _basketRepository.GetBasketAsync(BasketId);
            if (Basket is null) return null;
            var ShippingPrice = 0M; 
            if (Basket.DeliveryMethodId.HasValue)
            {
                var DeliveryMethod = await _unitOfWork.Repository<DeliveryMethod>().GetAsync(Basket.DeliveryMethodId.Value);
                ShippingPrice = DeliveryMethod.Cost;
            }
            if (Basket.Items.Count > 0)
            {
                foreach (var item in Basket.Items)
                {
                    var Product = await _unitOfWork.Repository<Core.Entities.Product>().GetAsync(item.Id);
                    if(item.Price != Product.Price)
                        item.Price = Product.Price;
                }
            }
            var SubTotal = Basket.Items.Sum(item => item.Price * item.Quantity );
            var Service = new PaymentIntentService();
            PaymentIntent paymentIntent;
            if (string.IsNullOrEmpty(Basket.PaymentIntentId))
            {
                var options = new PaymentIntentCreateOptions()
                {
                    Amount = (long)ShippingPrice * 100 + (long)SubTotal * 100,
                    Currency = "usd",
                    PaymentMethodTypes = new List<string>() { "card" }
                };
                paymentIntent = await Service.CreateAsync(options);
                Basket.PaymentIntentId = paymentIntent.Id;
                Basket.ClientSecret = paymentIntent.ClientSecret;
            }
            else
            {
                var options = new PaymentIntentUpdateOptions()
                {
                    Amount = (long)ShippingPrice * 100 + (long)SubTotal * 100
                };
                paymentIntent = await Service.UpdateAsync(Basket.PaymentIntentId, options);
                Basket.PaymentIntentId = paymentIntent.Id;
                Basket.ClientSecret = paymentIntent.ClientSecret;
            }
            await _basketRepository.UpdateBasketAsync(Basket);
            return Basket;
        }
    }
}
