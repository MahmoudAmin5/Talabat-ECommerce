
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.OrderAggregate;

namespace Talabat.Core.Specifications.OrderSpecs
{
    public class OrderSpecifications:BaseSpecification<Order>
    {
        public OrderSpecifications(string BuyerEmail):base(o=>o.BuyerEmail == BuyerEmail )
        {
            Includes.Add(o => o.DeliveryMethod);
            Includes.Add(O => O.Items);
            AddOrderByDesc( o => o.OrderDate);
        }
    }
}
