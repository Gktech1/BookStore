using Cart.Domain.Entities;

namespace Cart.Application.Repositories
{
    public interface ICartRepository
    {
        Task<ShoppingCart> GetCart(string userName);
        Task<ShoppingCart> UpdateCart(ShoppingCart basket);
        Task DeleteCart(string userName);
    }
}
