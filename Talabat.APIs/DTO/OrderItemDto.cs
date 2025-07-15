using Talabat.Core.OrderAggregate;

namespace Talabat.APIs.DTO
{
    public class OrderItemDto
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string PictureURL { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}