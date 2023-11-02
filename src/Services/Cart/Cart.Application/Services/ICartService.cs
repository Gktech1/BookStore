using Cart.Domain.DTOs;
using Cart.Domain.Entities;
using Shared.GenericResponse;
namespace Cart.Application.Services
{
    public interface ICartService
    {
        Task<GenericResponse<ShoppingCartRespone>> GetBasket(string userName);
        Task<ShoppingCart> UpdateBasket(ShoppingCart basket);
        Task DeleteBasket(string userName);
    }
}
