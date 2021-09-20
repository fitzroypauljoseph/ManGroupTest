using System;
using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Annotations;
using TradingPlaces.WebApi.Dtos;
using TradingPlaces.WebApi.Services;

namespace TradingPlaces.WebApi.Controllers
{
    [Route("api/strategy")]
    [ApiController]
    public class StrategyController : ControllerBase
    {
        private readonly IHostedServiceAccessor<IStrategyManagementService> _strategyManagementService;
        private readonly ILogger<StrategyController> _logger;

        public StrategyController(IHostedServiceAccessor<IStrategyManagementService> strategyManagementService, ILogger<StrategyController> logger)
        {
            _strategyManagementService = strategyManagementService;
            _logger = logger;
        }

        [HttpGet("{id}")]
        [SwaggerOperation(nameof(GetCurrentSharePrice))]
        [SwaggerResponse(StatusCodes.Status200OK, "OK")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Not Found")]
        public IActionResult GetCurrentSharePrice(string id)
        {
            try
            {
                var strategies = _strategyManagementService.GetCurrentSharePrice();
                _logger.LogInformation($"Share price information returned.");
                return Ok(strategies);
            }
            catch (Exception ex)
            {
                _logger.LogError($"The share price could not be found action: {ex.Message}");
                return StatusCode(StatusCodes.Status404NotFound);
            }
        }

        [HttpPost]
        [SwaggerOperation(nameof(RegisterStrategy))]
        [SwaggerResponse(StatusCodes.Status200OK, "OK", typeof(string))]
        public IActionResult RegisterStrategy(StrategyDetailsDto strategyDetails)
        {
            try
            {
                var strategies = _strategyManagementService.GetCurrentSharePrice();
                _logger.LogInformation($"Share price information returned.");
                return Ok(strategies);
            }
            catch (Exception ex)
            {
                _logger.LogError($"The share price could not be found action: {ex.Message}");
                return StatusCode(StatusCodes.Status404NotFound);
            }
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(nameof(UnregisterStrategy))]
        [SwaggerResponse(StatusCodes.Status200OK, "OK")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Not Found")]
        public IActionResult UnregisterStrategy(string id)
        {
            throw new NotImplementedException();
        }
    }
}
