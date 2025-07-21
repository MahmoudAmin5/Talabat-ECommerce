using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.OrderAggregate;

namespace Talabat.Core.Specifications.OrderSpecs
{
    public class OrderWithPaymentIntentIdSpecifications : BaseSpecification<Order>
    {
        public OrderWithPaymentIntentIdSpecifications(string PaymentIntentId):base(P=> P.PaymentIntentId == PaymentIntentId)
        {
            
        }

    }
}
