using System;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ToDoList.Model;
using ToDoList.Services;

namespace ToDoList.Controllers
{
    [ApiController]
    [Authorize]
    public class ToDoListController : ControllerBase
    {
        private readonly ILogger<ToDoListController> _logger;    
        private readonly IToDoListService _toDoListService;
        public ToDoListController(ILogger<ToDoListController> logger, IToDoListService toDoListService)
        {
            _logger = logger;
            _toDoListService = toDoListService;
        }

        [HttpGet("todolist/{id}")]
        public ActionResult GetTaskById(int id)
        {
            try
            {
                _logger.LogInformation("Entering getTaskById controller");
                var result = _toDoListService.GetTaskById(id);
                return Ok(result);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        [HttpGet("todolist/pending")]
        public ActionResult GetUserTasks()
        {
            try
            {
                _logger.LogInformation("Entering getUserTasks controller");
                var userId = Int32.Parse(User?.Identity?.Name);
                var result = _toDoListService.GetUserTasks(userId);
                return Ok(result);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpGet("todolist/completed")]
        public ActionResult GetUserCompletedTasks()
        {
            try
            {
                _logger.LogInformation("Entering getUserCompletedTasks controller");
                //var userId = Int32.Parse(User.FindFirstValue(ClaimTypes.Name));
                var userId = Int32.Parse(User?.Identity?.Name);
                var result = _toDoListService.GetUserCompletedTasks(userId);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        [HttpPost("todolist")]
        public  ActionResult CreateTask(Model.Task task)
        {
            try
            {

                _logger.LogInformation("Entering create controller");
                var userId = Int32.Parse(User?.Identity?.Name);
                _toDoListService.CreateTask(userId,task);
                return Ok("Created task succesfully");
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("todolist")]
        public  ActionResult UpdateStatus(int id)
        {
            try
            {
                _logger.LogInformation("Entering updateStatus controller");
                var userId = Int32.Parse(User?.Identity?.Name);
                _toDoListService.UpdateTask(id,userId);
                return Ok("Updated task successfully");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        [HttpDelete("todolist/{id}")]
        public  ActionResult DeleteTask(int id)
        {
            try
            {
                _logger.LogInformation("Entering deleteTask controller");
                var userId = Int32.Parse(User?.Identity?.Name);
                _toDoListService.DeleteTask(id,userId);
                return Ok("Deleted task Successfully");
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}

