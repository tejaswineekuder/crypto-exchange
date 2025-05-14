using crypto_exchange.DTOs;
using crypto_exchange.Services;
using crypto_exchange_web.DTOs;
using crypto_exchange_web.Services;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace crypto_exchange.Tests.Services
{
    public class ExecutionPlanServiceTests
    {
        private readonly ILogger<ExecutionPlanService> _logger;
        private readonly IOrderBookService _orderBookService;
        private readonly IExchangeExecutionService _exchangeExecutionService;

        public ExecutionPlanServiceTests()
        {
            _logger = Substitute.For<ILogger<ExecutionPlanService>>();
            _orderBookService = Substitute.For<IOrderBookService>();
            _exchangeExecutionService = Substitute.For<IExchangeExecutionService>();
        }

        [Test]
        public async Task GetExecutionPlans_ValidRequest_ReturnsExecutionPlans()
        {
            // Arrange
            var service = new ExecutionPlanService(_logger, _orderBookService, _exchangeExecutionService);
            var requestDto = new RequestDto
            {
                OrderAmount = 100,
                OrderType = "Buy"
            };

            _orderBookService.LoadOrderBooks(null).Returns(new List<ExchangeOrderBookDto>());
            _exchangeExecutionService.ExecuteOrder(Arg.Any<List<ExchangeOrderBookDto>>(), requestDto.OrderType, Convert.ToDecimal(requestDto.OrderAmount)).Returns(new List<ExecutionPlanDto>());

            // Act
            var result = await service.GetExecutionPlans(requestDto);

            // Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public async Task GetExecutionPlans_InvalidRequest_Returns0Plans()
        {
            //Arrange
            var service = new ExecutionPlanService(_logger, _orderBookService, _exchangeExecutionService);
            var requestDto = new RequestDto
            {
                OrderAmount = 100,
                OrderType = "Buy"
            };
            _orderBookService.LoadOrderBooks(null).ReturnsNull();
            _exchangeExecutionService.ExecuteOrder(null, requestDto.OrderType, Convert.ToDecimal(requestDto.OrderAmount)).ReturnsNull();

            // Act
            var result = await service.GetExecutionPlans(requestDto);

            // Assert
            Assert.NotNull(result);
            Assert.Zero(result.Count);
        }
    }
}
