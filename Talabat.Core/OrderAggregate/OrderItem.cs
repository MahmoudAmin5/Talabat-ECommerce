﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Core.OrderAggregate
{
    public class OrderItem: BaseEntity
    {
        public OrderItem()
        {
            
        }
        public OrderItem(ProductItemOrdered product, int quantity, decimal price)
        {
            this.product = product;
            Quantity = quantity;
            Price = price;
        }

        public ProductItemOrdered product { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }

    }
}
