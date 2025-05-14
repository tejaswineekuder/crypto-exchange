using crypto_exchange.DTOs;
using crypto_exchange.Services;
using crypto_exchange_web.DTOs;

namespace crypto_exchange_web.Services
{
    public interface IExecutionPlanService
    {
        Task<List<ExecutionPlanDto>> GetExecutionPlans(RequestDto requestDto);
    }
    public class ExecutionPlanService : IExecutionPlanService
    {
        private readonly ILogger<ExecutionPlanService> _logger;
        private readonly IOrderBookService _orderBookService;
        private readonly IExchangeExecutionService _exchangeExecutionService;
        public ExecutionPlanService(ILogger<ExecutionPlanService> logger, IOrderBookService orderBookService, IExchangeExecutionService exchangeExecutionService)
        {
            _logger = logger;
            _orderBookService = orderBookService;
            _exchangeExecutionService = exchangeExecutionService;
        }

        public async Task<List<ExecutionPlanDto>> GetExecutionPlans(RequestDto requestDto)
        {
            var result = new List<ExecutionPlanDto>();

            var loadOrderBooks = _orderBookService.LoadOrderBooks(null);
            if (loadOrderBooks == null)
            {
                _logger.LogError("Error reading Orderbooks!");
                return result;
            }

            if (loadOrderBooks?.Count == 0)
            {
                _logger.LogError("No asks/bid found in Orderbooks! ");
                return result;
            }

            result = _exchangeExecutionService.ExecuteOrder(loadOrderBooks, requestDto.OrderType, Convert.ToDecimal(requestDto.OrderAmount));
            return result;
        }
    }
}

