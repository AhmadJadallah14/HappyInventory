using HappyInventory.Helpers.Enum;
using HappyInventory.Services.LogService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HappyInventory.API.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Roles = $"{nameof(UserRole.Admin)}")]
    public class LogController : ControllerBase
    {

        private readonly ILogsService _logService;

        public LogController(ILogsService logService)
        {
            _logService = logService;
        }

        [HttpGet("get-logs")]
        public IActionResult GetLogs()
        {
            var logs = _logService.GetLogs();
            return Ok(logs);
        }
    }

}

