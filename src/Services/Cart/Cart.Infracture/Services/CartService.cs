

using AutoMapper;
using Cart.Application.Repositories;
using Cart.Application.Services;
using Cart.Domain.DTOs;
using Cart.Domain.Entities;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Shared.GenericResponse;
using System.Net;

namespace Cart.Infrastructure.Services
{
    public class CartService : ICartService
    {
        private readonly ICartRepository _cartRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<CartService> _logger;
        public CartService(ICartRepository cartRepository, IMapper mapper, ILogger<CartService> logger) 
        {
            _cartRepository = cartRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<GenericResponse<ShoppingCartRespone>> GetBasket(string userName)
        {
            _logger.LogInformation($"Inside GetBasket  Service Method:{GetBasket}");
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
                _logger.LogInformation($"The list of items gotten successfully: {JsonConvert.SerializeObject(items)}");
                return new GenericResponse<ShoppingCartRespone>
                {
                    ResponseMessage = "Item gotten successfully",
                    StatusCode = HttpStatusCode.OK,
                    Data = items
                };
            }
            catch (Exception ex) 
            {
                _logger.LogError($"an error occur while processing the request{ex.Message}");
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
                var updateCartItem = await _cartRepository.UpdateCart(updatedBasket);
                _logger.LogInformation($"The item gotten successfully: {JsonConvert.SerializeObject(updateCartItem)}");

                return new GenericResponse<ShoppingCartRespone>
                {
                    ResponseMessage = "Item Added successfully",
                    StatusCode = HttpStatusCode.OK,
                    Data = null
                };
            }
            catch (Exception ex)
            {
                _logger.LogError($"an error occur while processing the request{ex.Message}");
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
                await _cartRepository.DeleteCart(userName);

                _logger.LogInformation($"The item remove form the cart successfully");
                return new GenericResponse<ShoppingCartRespone>
                {
                    ResponseMessage = "Item deleted successfully",
                    StatusCode = HttpStatusCode.OK,
                    Data = null
                };

            }
            catch(Exception ex) 
            {
                _logger.LogError($"an error occur while processing the request{ex.Message}");
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

