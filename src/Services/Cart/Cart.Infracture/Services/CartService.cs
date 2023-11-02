

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
                var result = _cartRepository.GetCart(userName);
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

        public Task<ShoppingCart> UpdateBasket(ShoppingCart basket)
        {
            throw new NotImplementedException();
        }

        public Task DeleteBasket(string userName)
        {
            throw new NotImplementedException();
        }
    }
}
