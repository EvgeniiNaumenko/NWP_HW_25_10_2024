using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Threading.Tasks;

class Program
{
    static async Task Main(string[] args)
    {
        var connection = new HubConnectionBuilder()
            .WithUrl("https://localhost:7063/currencyHub")
            .Build();

        connection.On<string, double>("ReceiveRateUpdate", (currencyPair, rate) =>
        {
            Console.WriteLine($"Currency Pair: {currencyPair}, New Rate: {rate}");
        });

        try
        {
            await connection.StartAsync();
            Console.WriteLine("Connected to the server. Listening for currency rate updates...");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error connecting to server: {ex.Message}");
            return;
        }

        Console.WriteLine("Press Enter to exit.");
        Console.ReadLine();

        await connection.StopAsync();
        await connection.DisposeAsync();
    }
}
