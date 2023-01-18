using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ToDoList.Model;
using ToDoList.Services;
using Microsoft.AspNetCore.Authorization;

namespace ToDoList.Controllers
{
	[ApiController]
    [Authorize]
    public class AdminController : ControllerBase
	{
		private readonly IadminService _adminService;
        private readonly ILogger<AdminController> _logger;
        public AdminController(ILogger<AdminController> logger, IadminService adminService)
		{
            _adminService = adminService;
            _logger = logger;
		}
        

		[HttpPost("taskapproval")]
		public ActionResult approveTask(int id)
		{
			try
			{
                _logger.LogInformation("Entering approveTask controller");
                var userId = Int32.Parse(User?.Identity?.Name);
                _adminService.approveTask(userId, id);
				return Ok("Approved task successfully");
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}
        [HttpGet("tasks")]
        public ActionResult getAllTasks()
        {
            try
            {
                _logger.LogInformation("Entering getAllTasks controller");
                var userId = Int32.Parse(User?.Identity?.Name);
                var result = _adminService.getAllTasks(userId);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}

