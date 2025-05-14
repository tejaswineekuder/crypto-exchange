using crypto_exchange.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace crypto_exchange.Services
{
    public interface IExchangeExecutionService
    {
        List<ExecutionPlanDto> ExecuteOrder(List<ExchangeOrderBookDto> exchanges, string orderType, decimal amount);
    }
    public class ExchangeExecutionService : IExchangeExecutionService
    {
        public List<ExecutionPlanDto> ExecuteOrder(List<ExchangeOrderBookDto> exchanges, string orderType, decimal amount)
        {
            var result = new List<ExecutionPlanDto>();
            if (orderType.Equals("Buy", StringComparison.OrdinalIgnoreCase))
            {
                result = ExecuteBuyOrder(exchanges, amount);
            }
            else if (orderType.Equals("Sell", StringComparison.OrdinalIgnoreCase))
            {
                result = ExecuteSellOrder(exchanges, amount);
            }
            return result;
        }

        private List<ExecutionPlanDto> ExecuteBuyOrder(List<ExchangeOrderBookDto> exchanges, decimal amount)
        {
            var result = new List<ExecutionPlanDto>();
            var totalCost = 0m;
            var remainingAmount = amount;

            var allAsks = exchanges
                .SelectMany(e => e.OrderBook?.Asks?.Select(a => (Exchange: e?.Id, AvailableCrypto: e?.AvailableFunds?.Crypto, AvailableEuro: e.AvailableFunds?.Euro,
                                                                 Price: Convert.ToDecimal(a.Order?.Price), Amount: Convert.ToDecimal(a.Order?.Amount))))
                .OrderBy(a => a.Price)
                .ToList();

            foreach (var ask in allAsks)
            {
                if (remainingAmount <= 0) break;

                if (remainingAmount < ask.Amount) continue;

                var amountToBuy = Math.Min(remainingAmount, ask.Amount);

                var totalCostOfOrderBookInLoop = result.Where(x => x.Id == ask.Exchange).Sum(x => x.Price);
                var remainingBTCOfOrderBookInLoop = result.Where(x => x.Id == ask.Exchange).Sum(x => x.Amount);

                if (totalCostOfOrderBookInLoop >= ask.AvailableEuro || remainingBTCOfOrderBookInLoop >= ask.AvailableCrypto) continue;

                remainingAmount -= amountToBuy;
                totalCost += amountToBuy * ask.Price;
                result.Add(new ExecutionPlanDto { Type = "Buy", Amount = amountToBuy, Id = ask.Exchange, Price = ask.Price });
            }

            return result;
        }

        private List<ExecutionPlanDto> ExecuteSellOrder(List<ExchangeOrderBookDto> exchanges, decimal amount)
        {
            var result = new List<ExecutionPlanDto>();
            var totalRevenue = 0m;
            var remainingAmount = amount;

            var allBids = exchanges
                .SelectMany(e => e.OrderBook?.Bids?.Select(b => (Exchange: e.Id, AvailableCrypto: e?.AvailableFunds.Crypto, AvailableEuro: e.AvailableFunds?.Euro,
                                                                 Price: Convert.ToDecimal(b.Order.Price), Amount: Convert.ToDecimal(b.Order.Amount))))
                .OrderByDescending(b => b.Price)
                .ToList();

            foreach (var bid in allBids)
            {
                if (remainingAmount <= 0) break;

                if (remainingAmount < bid.Amount) continue;

                var amountToSell = Math.Min(remainingAmount, bid.Amount);

                var totalCostOfOrderBookInLoop = result.Where(x => x.Id == bid.Exchange).Sum(x => x.Price);
                var remainingBTCOfOrderBookInLoop = result.Where(x => x.Id == bid.Exchange).Sum(x => x.Amount);

                if (totalCostOfOrderBookInLoop >= bid.AvailableEuro || remainingBTCOfOrderBookInLoop >= bid.AvailableCrypto) continue;

                remainingAmount -= amountToSell;
                totalRevenue += amountToSell * bid.Price;
                result.Add(new ExecutionPlanDto { Type = "Sell", Amount = amountToSell, Id = bid.Exchange, Price = bid.Price });
            }
            return result;
        }
    }
}
