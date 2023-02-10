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
		private readonly IAdminService _adminService;
        private readonly IUserService _userService;
        private readonly ILogger<AdminController> _logger;
        public AdminController(ILogger<AdminController> logger, IAdminService adminService, IUserService userService)
		{
            _adminService = adminService;
            _userService = userService;
            _logger = logger;
		}


        [HttpPost("addUser"), Authorize]
        public ActionResult AddUser(UserModel user)
        {
            try
            {
                _logger.LogInformation("Entering addUser controller");
                _userService.AddUser(user);
                return Ok("Added user succesfully");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpPost("taskapproval")]
		public ActionResult ApproveTask(int id)
		{
			try
			{
                _logger.LogInformation("Entering approveTask controller");
                var userId = Int32.Parse(User?.Identity?.Name);
                _adminService.ApproveTask(userId, id);
				return Ok("Approved task successfully");
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}
        [HttpGet("tasks")]
        public ActionResult GetAllTasks()
        {
            try
            {
                _logger.LogInformation("Entering getAllTasks controller");
                var userId = Int32.Parse(User?.Identity?.Name);
                var result = _adminService.GetAllTasks(userId);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpPut("updateStatus")]
        public ActionResult UpdateStatus(int id, Status status)
        {
            try
            {
                _logger.LogInformation("Entering updateStatus controller");
                var userId = Int32.Parse(User?.Identity?.Name);
                _adminService.UpdateTask(id,status,userId);
                return Ok("Updated task successfully");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpGet("users")]
        public ActionResult GetAllUsers()
        {
            try
            {
                _logger.LogInformation("Entering getAllUsers controller");
                var userId = Int32.Parse(User?.Identity?.Name);
                var users = _adminService.GetAllUsers(userId);
                return Ok(users);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpDelete("{id}")]
        public ActionResult DeleteTask(int id)
        {
            try
            {
                _logger.LogInformation("Entering deleteTask controller");
                var userId = Int32.Parse(User?.Identity?.Name);
                _adminService.DeleteTask(id, userId);
                return Ok("Deleted task Successfully");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}

