using crypto_exchange_web.DTOs;
using crypto_exchange_web.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace crypto_exchange_web.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class ExecutiobPlanController : ControllerBase
    {
        private readonly ILogger<ExecutiobPlanController> _logger;
        private readonly IExecutionPlanService _executionPlanService;

        public ExecutiobPlanController(ILogger<ExecutiobPlanController> logger, IExecutionPlanService executionPlanService)
        {
            _logger = logger;
            _executionPlanService = executionPlanService;
        }

        [HttpPost("execute")]
        public async Task<IActionResult> ExecutePlan([FromForm] RequestDto request)
        {
            if (request.OrderAmount == null || request.OrderAmount == 0)
            {
                _logger.LogError("Order amount is invalid, cannot be processed further");
                return BadRequest("Invalid Order amount.");
            }

            if (string.IsNullOrWhiteSpace(request.OrderType))
            {
                _logger.LogError("Order type is invalid, cannot be processed further");
                return BadRequest("Invalid Order type.");
            }
            _logger.LogInformation("Input verified , moving further to fetch execution plan");

            var executionPlan = await _executionPlanService.GetExecutionPlans(request);
            if (executionPlan == null)
            {
                _logger.LogError("Error fetching execution plans!");
                return BadRequest("Execution plans not found.");
            }

            _logger.LogInformation("Invoice evaluation summary : " + JsonConvert.SerializeObject(executionPlan));
            return Ok(new
            {
                ExecutionPlan = executionPlan
            });
        }
    }
}
