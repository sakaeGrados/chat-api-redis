namespace BDII_TP.Hubs
{
    using Microsoft.AspNetCore.SignalR;

    public class ChatHub : Hub
    {
        public async Task SendMessage(object message)
        {
            await Clients.All.SendAsync("ReceiveMessage", message);
        }
    }
}
