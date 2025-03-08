using AutoMapper;
using Talabat.Core.Entities;

namespace Talabat.APIs.DTO
{
    public class ProductToReturnDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string PictureUrl { get; set; }
        public decimal Price { get; set; }
       
        public int BrandId { get; set; } 
        public String Brand { get; set; }
        
        public int CategoryId { get; set; }
        public String Category { get; set; }
    }
}
