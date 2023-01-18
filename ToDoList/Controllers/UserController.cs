using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ToDoList.Model;
using ToDoList.Services;

namespace ToDoList.Controllers
{
    [ApiController]
    public class Usercontroller : ControllerBase
    {

        private readonly ToDoListDBContext _toDoListDBContext;
        private readonly IuserService _userService;
        private readonly ILogger<ToDoListController> _logger;

        public Usercontroller(ILogger<ToDoListController> logger,ToDoListDBContext toDoListDBContext, IuserService userService)
        {

            _toDoListDBContext = toDoListDBContext;
            _userService = userService;
            _logger = logger;
        }

        [HttpPost("addUser")]
        public ActionResult addUser(userModel user)
        {
            try
            {
                _logger.LogInformation("Entering addUser controller");
                _userService.addUser(user);
                return Ok("Added user succesfully");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("login")]
        public  ActionResult login(loginRequestModel request)
        {
            try
            {
                _logger.LogInformation("Entering login controller");
                var token = _userService.login(request);
                return Ok(token);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}