using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
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
    public class OrderService : IOrderService
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IUnitOfWork _unitOfWork;

        public OrderService(IBasketRepository basketRepository, IUnitOfWork unitOfWork)
        {
            _basketRepository = basketRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<Order?> CreateOrderAsync(string BuyerEmail, string BasketId, int DeliveryMethodId, Address ShippingAddress)
        {
            // Get Basket 
            var Basket = await _basketRepository.GetBasketAsync(BasketId);
            // Get Selected Items Form Basket
            var OrderItems = new List<OrderItem>();
            if (Basket?.Items.Count() > 0)
            {
                foreach (var item in Basket.Items)
                {
                    var Product = await _unitOfWork.Repository<Product>().GetAsync(item.Id);
                    var ProductOrderItem = new ProductItemOrdered(Product.Id ,Product.Name,Product.PictureUrl);
                    var OrderItem = new OrderItem(ProductOrderItem, item.Quantity, Product.Price);
                    OrderItems.Add(OrderItem);
                }
            }
            var SubTotal = OrderItems.Sum(Item => Item.Quantity * Item.Price);
            // Get DeliveryMethod 
            var deliveryMethod = await _unitOfWork.Repository<DeliveryMethod>().GetAsync(DeliveryMethodId);
            // create Order 
            var Order = new Order(BuyerEmail, ShippingAddress, deliveryMethod, OrderItems, SubTotal); 
            // Add Order Locally 
            await _unitOfWork.Repository<Order>().AddAsync(Order);
            //Add to Database
            var RowsAffected = await _unitOfWork.CompleteAsync();
            if (RowsAffected <= 0) return null;
            return Order; 
    
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
