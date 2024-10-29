using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;
using Server_HW_25_10_2024.Hubs;

class Program
{
    public static string[] currencyPairs = new string[] { "USD/EUR", "GBP/EUR" };
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddSignalR();

        var app = builder.Build();

        app.MapGet("/", () => "Hello!");

        app.MapHub<CurrencyHub>("/currencyHub");

        UpdateRates( app.Services);
        app.Run();

    }
    private static void UpdateRates(IServiceProvider service)
    {
        var random = new Random();
        var hubContext = service.GetRequiredService<IHubContext<CurrencyHub>>();
        new Thread(() =>
        {
            while (true)
            {
                foreach (var pair in currencyPairs)
                {
                    var rate = Math.Round(random.NextDouble() * 2 + 0.5, 4);
                    hubContext.Clients.All.SendAsync("ReceiveRateUpdate", pair, rate);
                }
                Thread.Sleep(3000);
            }
        }).Start();
    }
}

