using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace SggApp.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HealthController : ControllerBase
    {
        private readonly HealthCheckService _healthCheckService;

        public HealthController(HealthCheckService healthCheckService)
        {
            _healthCheckService = healthCheckService;
        }

        [HttpGet]
        public async Task<IActionResult> GetHealthAsync()
        {
            var result = await _healthCheckService.CheckHealthAsync();

            if (result.Status == HealthStatus.Healthy)
            {
                return Ok(new
                {
                    Status = "Healthy",
                    Message = "API está activa",
                    Details = result.Entries.Select(e => new
                    {
                        e.Key,
                        Status = e.Value.Status.ToString(),
                        e.Value.Description
                    })
                });
            }

            return StatusCode(503, new
            {
                Status = "Unhealthy",
                Message = "Algunas dependencias fallaron",
                Details = result.Entries.Select(e => new
                {
                    e.Key,
                    Status = e.Value.Status.ToString(),
                    e.Value.Description
                })
            });
        }
    }
}