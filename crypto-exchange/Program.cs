using crypto_exchange.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;


var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((_, services) =>
    {
        services.AddSingleton<IOrderBookService, OrderBookService>();
        services.AddSingleton<IExchangeExecutionService, ExchangeExecutionService>();
    })
    .Build();

using var scope = host.Services.CreateScope();
var services = scope.ServiceProvider;

var orderBookService = services.GetRequiredService<IOrderBookService>();
var executionService = services.GetRequiredService<IExchangeExecutionService>();

string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "Exchanges");
var orderBooks = orderBookService.LoadOrderBooks(folderPath);

Console.WriteLine($"Loaded {orderBooks.Count} exchanges.");

Console.Write("Enter Order Type (Buy/Sell): ");
var orderType = Console.ReadLine()?.Trim();

Console.Write("Enter Amount of BTC: ");
var amountInput = Console.ReadLine()?.Trim();
if (!decimal.TryParse(amountInput, out var amount) || amount <= 0)
{
    Console.WriteLine("Invalid amount. Please enter a positive number.");
    return;
}

var result = executionService.ExecuteOrder(orderBooks, orderType ?? "", amount);
Console.WriteLine("\n--- Best Execution Plan ---");
foreach (var line in result)
{
    Console.WriteLine(line.Plan);
}
