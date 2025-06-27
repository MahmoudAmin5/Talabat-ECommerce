using System.ComponentModel.DataAnnotations;

namespace Talabat.APIs.DTO
{
    public class BasketItemDto
    {
        [Required]
        public int Id { get; set; }

        public required string ProductName { get; set; }
   
        public required string PictureURL { get; set; }
        
        public required string Brand { get; set; }
       
        public required string Type { get; set; }
        [Required]
        [Range(0.1, double.MaxValue , ErrorMessage = "Price must be greater than Zero !")]
        public decimal Price { get; set; }
        [Required]
        [Range(1, int.MaxValue , ErrorMessage = "Quantity must be at least 1 Item")]
        public int Quantity { get; set; }
    }
}