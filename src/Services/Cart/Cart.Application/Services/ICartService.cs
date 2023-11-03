using Cart.Domain.DTOs;
using Cart.Domain.Entities;
using Shared.GenericResponse;
namespace Cart.Application.Services
{
    public interface ICartService
    {
        Task<GenericResponse<ShoppingCartRespone>> GetBasket(string userName);
        Task<GenericResponse<ShoppingCartRespone>> UpdateBasket(UpdateShoppingCartDto updateBasket);
        Task<GenericResponse<ShoppingCartRespone>> DeleteBasket(string userName);
    }
}
