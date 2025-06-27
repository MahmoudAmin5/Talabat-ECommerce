using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Entities
{
    public class Product:BaseEntity
    {
        public required string Name { get; set; }
        public required string Description { get; set; }
        public required string PictureUrl { get; set; }
        public decimal Price { get; set; }
        //Fluent API
        public int BrandId { get; set; } // Foregin Key
        //Nav Props
        public ProductBrand Brand { get; set; } 
        //Fluent API 
        public int CategoryId { get; set; }
        public ProductCategory Category { get; set; }

    }
}
