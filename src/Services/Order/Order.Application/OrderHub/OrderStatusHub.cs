

using Microsoft.AspNetCore.SignalR;

namespace Application.OrderHub
{
    public class OrderStatusHub : Hub
    {
        public async Task SendOrderStatusUpdate(int orderId, string status)
        {
            await Clients.All.SendAsync("ReceiveOrderStatusUpdate", orderId, status);
        }

    }
}

