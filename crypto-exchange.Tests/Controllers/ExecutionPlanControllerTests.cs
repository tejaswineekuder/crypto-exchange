using crypto_exchange.DTOs;
using crypto_exchange_web.Controllers;
using crypto_exchange_web.DTOs;
using crypto_exchange_web.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace crypto_exchange.Tests.Controllers
{
    public class ExecutionPlanControllerTests
    {
        private readonly ILogger<ExecutiobPlanController> _logger;
        private readonly IExecutionPlanService _executionPlanService;

        public ExecutionPlanControllerTests()
        {
            _logger = Substitute.For<ILogger<ExecutiobPlanController>>();
            _executionPlanService = Substitute.For<IExecutionPlanService>();
        }

        [Test]

        public async Task ExecutePlan_ValidRequest_ReturnsOkResult()
        {
            // Arrange
            var controller = new ExecutiobPlanController(_logger, _executionPlanService);
            var request = new RequestDto
            {
                OrderAmount = 100,
                OrderType = "Buy"
            };
            _executionPlanService.GetExecutionPlans(request).Returns( new List<ExecutionPlanDto>());

            // Act
            var result = await controller.ExecutePlan(request);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
        }

        [Test]

        public async Task ExecutePlan_ValidRequest_ReturnsBadResult()
        {
            // Arrange
            var controller = new ExecutiobPlanController(_logger, _executionPlanService);
            var request = new RequestDto
            {
                OrderAmount = 100,
                OrderType = "Buy"
            };

            _executionPlanService.GetExecutionPlans(request).ReturnsNull();


            // Act
            var result = await controller.ExecutePlan(request);

            // Assert
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
        }

        [Test]

        public async Task ExecutePlan_InValidRequest_ReturnsBadResult()
        {
            // Arrange
            var controller = new ExecutiobPlanController(_logger, _executionPlanService);
            var request = new RequestDto
            {
                OrderAmount = 100,
                OrderType = "Buyer"
            };

            _executionPlanService.GetExecutionPlans(request).ReturnsNull();


            // Act
            var result = await controller.ExecutePlan(request);

            // Assert
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
        }
    }
}
