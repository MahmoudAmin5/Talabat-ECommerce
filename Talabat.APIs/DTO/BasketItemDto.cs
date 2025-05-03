using System.ComponentModel.DataAnnotations;

namespace Talabat.APIs.DTO
{
    public class BasketItemDto
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string ProductName { get; set; }
        [Required]
        public string PictureURL { get; set; }
        [Required]
        public string Brand { get; set; }
        [Required]
        public string Type { get; set; }
        [Required]
        [Range(0.1, double.MaxValue , ErrorMessage = "Price must be greater than Zero !")]
        public decimal Price { get; set; }
        [Required]
        [Range(1, int.MaxValue , ErrorMessage = "Quantity must be at least 1 Item")]
        public int Quantity { get; set; }
    }
}