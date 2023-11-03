using Cart.Domain.Entities;


namespace Cart.Domain.DTOs
{
    public class UpdateShoppingCartDto
    {
        public string UserName { get; set; }
        public List<ShoppingCartItem> Items { get; set; }
    }
}
