

namespace Product.Domain.Entities
{
    public class Product
    {
        public int ProductId { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
    }
}
