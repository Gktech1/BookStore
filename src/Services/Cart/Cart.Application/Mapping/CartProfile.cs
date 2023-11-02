﻿

using AutoMapper;
using Cart.Domain.DTOs;
using Cart.Domain.Entities;
using EventBus.Messages.Events;

namespace Cart.Application.Mapping
{
    public class CartProfile : Profile
    {
        public CartProfile()
        {
            CreateMap<CartCheckout, CartCheckoutEvent>().ReverseMap();
            CreateMap<ShoppingCart, ShoppingCartRespone>().ReverseMap(); 

        }
    }
}
