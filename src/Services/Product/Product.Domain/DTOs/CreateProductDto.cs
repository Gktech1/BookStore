

namespace Domain.DTOs
{
    public class CreateProductDto
    {
        public string? ProductId { get; set; }
        public string? Title { get; set; }
        public string? Author { get; set; }
        public string? Category { get; set; }
        public decimal? Price { get; set; }
        public string? Summary { get; set; }
        public string? Description { get; set; }
        public string? ImageFile { get; set; }
        public long? StockQuantity { get; set; } = 0;
    }
}
