using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.OrderAggregate
{
    public class ProductItemOrdered
    {
        public ProductItemOrdered()
        {
            
        }
        public ProductItemOrdered(int productId, string productName, string pictureURL)
        {
            ProductId = productId;
            ProductName = productName;
            PictureURL = pictureURL;
        }

        public int ProductId { get; set; }
        public required string ProductName { get; set; }
        public required string PictureURL { get; set; }
    }
}
