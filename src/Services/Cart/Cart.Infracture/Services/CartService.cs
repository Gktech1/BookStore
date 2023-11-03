

using AutoMapper;
using Cart.Application.Repositories;
using Cart.Application.Services;
using Cart.Domain.DTOs;
using Cart.Domain.Entities;
using Shared.GenericResponse;
using System.Net;

namespace Cart.Infrastructure.Services
{
    public class CartService : ICartService
    {
        private readonly ICartRepository _cartRepository;
        private readonly IMapper _mapper;
        public CartService(ICartRepository cartRepository, IMapper mapper) 
        {
            _cartRepository = cartRepository;
            _mapper = mapper;
        }

        public async Task<GenericResponse<ShoppingCartRespone>> GetBasket(string userName)
        {
            try
            {
                var result = await _cartRepository.GetCart(userName);
                if (result == null)
                {
                    return new GenericResponse<ShoppingCartRespone>
                    {
                        ResponseMessage = "Item can not be found",
                        StatusCode = HttpStatusCode.NotFound,
                        Data = null
                    };

                } 
                var items = _mapper.Map<ShoppingCartRespone>(result);
                items.TotalPrice = TotalPrice(result.Items);
                return new GenericResponse<ShoppingCartRespone>
                {
                    ResponseMessage = "Item gotten successfully",
                    StatusCode = HttpStatusCode.OK,
                    Data = items
                };
            }
            catch (Exception ex) 
            {
                return new GenericResponse<ShoppingCartRespone>
                {
                    ResponseMessage = $"an error occur while processing the request{ex.Message}",
                    StatusCode = HttpStatusCode.InternalServerError,
                    Data = null
                };
            }
            
        }

        public async Task<GenericResponse<ShoppingCartRespone>> UpdateBasket(UpdateShoppingCartDto updateBasket)
        {
            try
            {
               
                if (updateBasket == null)
                {
                    return new GenericResponse<ShoppingCartRespone>
                    {
                        ResponseMessage = "Item can not be found",
                        StatusCode = HttpStatusCode.NotFound,
                        Data = null
                    };

                }

                var cart = new ShoppingCart();
                var updatedBasket = _mapper.Map<ShoppingCart>(updateBasket);
                var getCart = _cartRepository.UpdateCart(updatedBasket);
                
                return new GenericResponse<ShoppingCartRespone>
                {
                    ResponseMessage = "Item updated successfully",
                    StatusCode = HttpStatusCode.OK,
                    Data = null
                };
            }
            catch (Exception ex)
            {
                return new GenericResponse<ShoppingCartRespone>
                {
                    ResponseMessage = $"an error occur while processing the request{ex.Message}",
                    StatusCode = HttpStatusCode.InternalServerError,
                    Data = null
                };
            }
        }

        public async Task<GenericResponse<ShoppingCartRespone>> DeleteBasket(string userName)
        {
            try
            {
                var removeItemFromCart = _cartRepository.DeleteCart(userName);

                if (removeItemFromCart == null)
                {
                    return new GenericResponse<ShoppingCartRespone>
                    {
                        ResponseMessage = "Item is removed already",
                        StatusCode = HttpStatusCode.NotFound,
                        Data = null
                    };

                }
                return new GenericResponse<ShoppingCartRespone>
                {
                    ResponseMessage = "Item deleted successfully",
                    StatusCode = HttpStatusCode.OK,
                    Data = null
                };

            }
            catch(Exception ex) 
            {
                return new GenericResponse<ShoppingCartRespone>
                {
                    ResponseMessage = $"an error occur while processing the request{ex.Message}",
                    StatusCode = HttpStatusCode.InternalServerError,
                    Data = null
                };
            }
           
        }

        private decimal TotalPrice(List<ShoppingCartItem> Items)
        {
                decimal totalprice = 0;
                foreach (var item in Items)
                {
                    totalprice += item.Price * item.Quantity;
                }
                return totalprice;
            
        }
    }
}

