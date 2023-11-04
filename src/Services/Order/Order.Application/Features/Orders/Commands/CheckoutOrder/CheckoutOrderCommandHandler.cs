

using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Application.Contracts.Persistence;
using Domain.Entities;
using Microsoft.AspNetCore.SignalR;
using Application.OrderHub;

namespace Application.Features.Orders.Commands.CheckoutOrder
{
    public class CheckoutOrderCommandHandler : IRequestHandler<CheckoutOrderCommand, int>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<CheckoutOrderCommandHandler> _logger;
        private readonly IHubContext<OrderStatusHub> _hubContext;

        public CheckoutOrderCommandHandler(IOrderRepository orderRepository, IMapper mapper, 
            ILogger<CheckoutOrderCommandHandler> logger, IHubContext<OrderStatusHub> hubContext)
        {
            _orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _hubContext = hubContext;
        }

        public async Task<int> Handle(CheckoutOrderCommand request, CancellationToken cancellationToken)
        {
            //Random random = new Random();
            // Generate a random integer between 0 and 100 (exclusive)
            //int randomNumber = random.Next(0, 100);
            
            var orderEntity = _mapper.Map<Order>(request); 
            var newOrder = await _orderRepository.AddAsync(orderEntity);

            _logger.LogInformation($"Order {newOrder.OrderId} is successfully created.");

            //await SendMail(newOrder);
            await _hubContext.Clients.All.SendAsync("ReceiveOrderStatusUpdate", newOrder.OrderId.ToString(), "Processing");
            _logger.LogInformation($"Real time message is sent to this: {newOrder.OrderId}.");

            return newOrder.OrderId;

        }

    }
}
