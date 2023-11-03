using System;


namespace Cart.Domain.Entities
{
    public class ShoppingCart
    {
        public string UserName { get; set; }
        public string ProductId { get; set; }
        public List<ShoppingCartItem> Items { get; set; } = new List<ShoppingCartItem>();
    }
}
