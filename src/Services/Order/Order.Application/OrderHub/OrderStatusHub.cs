

using Microsoft.AspNetCore.SignalR;

namespace Application.OrderHub
{
    public class OrderStatusHub : Hub
    {
        public async Task SendOrderStatusUpdate(string orderId, string status)
        {
            await Clients.All.SendAsync("ReceiveOrderStatusUpdate", orderId, status);
        }

    }
}

