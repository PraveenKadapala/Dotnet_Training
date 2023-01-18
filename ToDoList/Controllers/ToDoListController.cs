using System;
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
        private readonly ItoDoListService _toDoListService;
        public ToDoListController(ILogger<ToDoListController> logger, ItoDoListService toDoListService)
        {
            _logger = logger;
            _toDoListService = toDoListService;
        }

        [HttpGet("todolist/{id}")]
        public ActionResult getTaskById(int id)
        {
            try
            {
                _logger.LogInformation("Entering getTaskById controller");
                var result = _toDoListService.getTaskById(id);
                return Ok(result);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        [HttpGet("todolist")]
        public ActionResult getUserTasks()
        {
            try
            {
                _logger.LogInformation("Entering getUserTasks controller");
                var userId = Int32.Parse(User?.Identity?.Name);
                var result = _toDoListService.getUserTasks(userId);
                return Ok(result);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        [HttpPost("todolist")]
        public  ActionResult create(task task)
        {
            try
            {

                _logger.LogInformation("Entering create controller");
                var userId = Int32.Parse(User?.Identity?.Name);
                _toDoListService.createTask(userId,task);
                return Ok("Created task succesfully");
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("todolist")]
        public  ActionResult updateStatus(int id)
        {
            try
            {
                _logger.LogInformation("Entering updateStatus controller");
                _toDoListService.updateTask(id);
                return Ok("Updated task successfully");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        [HttpDelete("todolist/{id}")]
        public  ActionResult deleteTask(int id)
        {
            try
            {
                _logger.LogInformation("Entering deleteTask controller");
                _toDoListService.deleteTask(id);
                return Ok("Deleted task Successfully");
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }


    }
}

