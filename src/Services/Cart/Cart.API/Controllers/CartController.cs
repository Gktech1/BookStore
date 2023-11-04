﻿using AutoMapper;
using Cart.Application.Services;
using Cart.Domain.DTOs;
using Cart.Domain.Entities;
using EventBus.Messages.Events;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Cart.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CartController : ControllerBase
    {
        private readonly ILogger<CartController> _logger;
        private readonly ICartService _cartService;
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly IMapper _mapper;

        public CartController(ICartService cartService,
            ILogger<CartController> logger, IMapper mapper, IPublishEndpoint publishEndpoint)
        {
            _cartService = cartService;
            _publishEndpoint = publishEndpoint ?? throw new ArgumentNullException(nameof(publishEndpoint));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger;
        }

        [HttpGet("{userName}", Name = "GetBasket")]
        [ProducesResponseType(typeof(ShoppingCartRespone), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ShoppingCartRespone>> GetBasket(string userName)
        {
            _logger.LogInformation($"Inside GetBasket Method:{GetBasket}");
            var basket = await _cartService.GetBasket(userName);
            return Ok(basket);
        }

        [HttpPost("AddItemToCart")]
        [ProducesResponseType(typeof(ShoppingCartRespone), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ShoppingCartRespone>> AddItemToCart([FromBody] UpdateShoppingCartDto updateBasketRequest)
        {
            _logger.LogInformation($"Inside AddItemToCart Method :{AddItemToCart}");

            return Ok(await _cartService.UpdateBasket(updateBasketRequest));

        }

        [HttpDelete("{userName}", Name = "DeleteBasket")]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteBasket(string userName)
        {
            _logger.LogInformation($"Inside DeleteBaske Method:{DeleteBasket}");
            await _cartService.DeleteBasket(userName);
            return Ok();
        }

        [Route("[action]")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.Accepted)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Checkout([FromBody] CartCheckoutDto cartCheckout)
        {
            _logger.LogInformation($"Inside GeCheckout Method:{Checkout}");
            // get existing basket with total price 
            // Create basketCheckoutEvent -- Set TotalPrice on basketCheckout eventMessage
            // send checkout event to rabbitmq
            // remove the basket

            // get existing basket with total price
            var cart = await _cartService.GetBasket(cartCheckout.UserName);
            if (cart == null)
            {
                return BadRequest();
            }

            // send checkout event to rabbitmq
            var eventMessage = _mapper.Map<CartCheckoutEvent>(cartCheckout);
            eventMessage.TotalPrice = cart.Data.TotalPrice;
            await _publishEndpoint.Publish(eventMessage);

            // remove the basket
            await _cartService.DeleteBasket(cart.Data.UserName);

            return Accepted();
        }
    }
}
