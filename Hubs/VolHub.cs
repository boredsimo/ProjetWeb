using Microsoft.AspNetCore.SignalR;
namespace ProjetWeb.Hubs
{
    public class VolHub:Hub
    {
        public async Task SendMessage()
        {
            await Clients.All.SendAsync("VolChange");
        }
    }
}
