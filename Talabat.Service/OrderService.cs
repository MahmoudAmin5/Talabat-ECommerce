using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.OrderAggregate;
using Talabat.Core.Repository.Core;
using Talabat.Core.Services.Core;

namespace Talabat.Service
{
    public class OrderService : IOrderService
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IGenericRepository<Product> _productRepo;
        private readonly IGenericRepository<DeliveryMethod> _DeliveryMethodRepo;
        private readonly IGenericRepository<Order> _orderRepo;

        public OrderService(IBasketRepository basketRepository, 
            IGenericRepository<Product> ProductRepo, 
            IGenericRepository<DeliveryMethod> DeliveryMethodRepo,
            IGenericRepository<Order> OrderRepo)
        {
            _basketRepository = basketRepository;
            _productRepo = ProductRepo;
            _DeliveryMethodRepo = DeliveryMethodRepo;
            _orderRepo = OrderRepo;
        }
        public async Task<Order> CreateOrderAsync(string BuyerEmail, string BasketId, int DeliveryMethodId, Address ShippingAddress)
        {
            // Get Basket 
            var Basket = await _basketRepository.GetBasketAsync(BasketId);
            // Get Selected Items Form Basket
            var OrderItems = new List<OrderItem>();
            if (Basket?.Items.Count() > 0)
            {
                foreach (var item in Basket.Items)
                {
                    var Product = await _productRepo.GetAsync(item.Id);
                    var ProductOrderItem = new ProductItemOrdered(Product.Id ,Product.Name,Product.PictureUrl);
                    var OrderItem = new OrderItem(ProductOrderItem, item.Quantity, Product.Price);
                    OrderItems.Add(OrderItem);
                }
            }
            var SubTotal = OrderItems.Sum(Item => Item.Quantity * Item.Price);
            // Get DeliveryMethod 
            var deliveryMethod = await _DeliveryMethodRepo.GetAsync(DeliveryMethodId);
            // create Order 
            var Order = new Order(BuyerEmail, ShippingAddress, deliveryMethod, OrderItems, SubTotal); 
            // Add Order Locally 
            await _orderRepo.AddAsync(Order);
            //Add to Database
    
        }

        public Task<Order> GetOrdersByIdForSpecificUserAsync(string BuyerEmail, int OrderId)
        {
            throw new NotImplementedException();
        }

        public Task<Order> GetOrdersForSpecificUserAsync(string BuyerEmail)
        {
            throw new NotImplementedException();
        }
    }
}
