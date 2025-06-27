using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.OrderAggregate;

namespace Talabat.Core.Services.Core
{
    public interface IOrderService
    {
        Task<Order> CreateOrderAsync(string BuyerEmail, string BasketId, int DeliveryMethodId, Address ShippingAddress);
        Task<Order> GetOrdersForSpecificUserAsync(string BuyerEmail);
        Task<Order> GetOrdersByIdForSpecificUserAsync(string BuyerEmail, int OrderId);

    }
}
