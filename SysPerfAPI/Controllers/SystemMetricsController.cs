using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SysMetricsAPI.Models;
using SysMetricsAPI.Services.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SysMetricsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SystemMetricsController : ControllerBase
    {
        private readonly ISystemMetricsService _systemMetricsService;
        private readonly ILogger<SystemMetricsController> _logger;

        public SystemMetricsController(ISystemMetricsService systemMetricsService, ILogger<SystemMetricsController> logger)
        {
            _systemMetricsService = systemMetricsService;
            _logger = logger;
        }


        /// <summary>
        /// Handles HTTP POST requests to process system metrics data received from the agent.
        /// </summary>
        /// <param name="monitoringResponse">The system metrics data sent in the request body.</param>
        /// <returns>
        /// An IActionResult indicating the result of the operation:
        /// - 200 OK with the status indicating success or error if data retrieval is successful.
        /// - 500 Internal Server Error with a generic error message if an exception occurs.
        /// </returns>
        [HttpPost("SystemMetricsDataFromAgent")]
        public IActionResult SystemMetricsDataFromAgent([FromBody] SystemMetricsData monitoringResponse)
        {
            string status = "";
            try
            {
                status = _systemMetricsService.SystemMetricsDataFromAgent(monitoringResponse);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while processing system metrics data.");
                return StatusCode(500, "An error occurred while processing your request.");
            }
            
            return Ok(status);
        }

        [HttpGet("GetSystemMetricsData")]
        public IActionResult GetSystemMetricsData()
        {
            SystemMetricsData sysMetricsData;
            try
            {
                sysMetricsData = _systemMetricsService.GetSystemMetricsData();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while processing system metrics data.");
                return StatusCode(500, "An error occurred while processing your request.");
            }

            return Ok(sysMetricsData);
        }

    }
}

