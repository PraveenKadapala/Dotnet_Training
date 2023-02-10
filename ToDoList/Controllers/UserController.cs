using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authorization;
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
        private readonly IUserService _userService;
        private readonly ILogger<ToDoListController> _logger;

        public Usercontroller(ILogger<ToDoListController> logger,ToDoListDBContext toDoListDBContext, IUserService userService)
        {

            _toDoListDBContext = toDoListDBContext;
            _userService = userService;
            _logger = logger;
        }

        [HttpPost("login")]
        public  ActionResult Login(loginRequestModel request)
        {
            try
            {
                _logger.LogInformation("Entering login controller");
                var token = _userService.Login(request);
                return Ok(token);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}