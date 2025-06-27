using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Entities
{
    public class BasketItem
    {
        public int Id { get; set; }
        public  required string ProductName { get; set; }
        public required string PictureURL { get; set; }
        public required string Brand { get; set; }
        public required string Type { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
