using Microsoft.AspNetCore.SignalR;

namespace Server_HW_25_10_2024.Hubs
{
    public class CurrencyHub : Hub
    {
        public async Task SendRateUpdate(string currencyPair, double rate)
        {
            await Clients.All.SendAsync("ReceiveRateUpdate", currencyPair, rate);
        }
    }
}
