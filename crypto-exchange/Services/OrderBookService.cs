using crypto_exchange.DTOs;
using Microsoft.Extensions.FileProviders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace crypto_exchange.Services
{
    public interface IOrderBookService
    {
        List<ExchangeOrderBookDto> LoadOrderBooks(string? folderPath);
    }

    public class OrderBookService : IOrderBookService
    {
        public List<ExchangeOrderBookDto> LoadOrderBooks(string? folderPath = null)
        {
            if (folderPath == null)
            {
                folderPath = Path.Combine(Directory.GetCurrentDirectory(), "Exchanges");
            }
            var orderBooks = new List<ExchangeOrderBookDto>();

            foreach (var file in Directory.GetFiles(folderPath, "*.json"))
            {
                var jsonData = File.ReadAllText(file);
                var exchangeOrderBook = JsonSerializer.Deserialize<ExchangeOrderBookDto>(jsonData);
                if (exchangeOrderBook != null)
                    orderBooks.Add(exchangeOrderBook);
            }

            return orderBooks;
        }
    }
}
