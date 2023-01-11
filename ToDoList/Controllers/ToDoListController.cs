using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ToDoList.Model;
using ToDoList.Services;

namespace ToDoList.Controllers
{
    [ApiController]
    //[Authorize]
    public class ToDoListController : ControllerBase
    {
        private readonly ToDoListDBContext _DbContext;
        private readonly ItoDoListService _toDoListService;
        public ToDoListController(ToDoListDBContext toDoListDBContext, ItoDoListService toDoListService)
        {
            _DbContext = toDoListDBContext;
            _toDoListService = toDoListService;
        }

        [HttpGet("todolist/{id}"), AllowAnonymous]
        public IActionResult getTaskById(int id)
        {
            try
            {
                var result = _toDoListService.getTask(id);
                return Ok(result);
            }
            catch(Exception e)
            {
                return BadRequest("Some error occured " + e.Message);
            }
        }


        [HttpGet("todolist")]
        public ActionResult getAllTasks()
        {
            try
            {
                var result = _toDoListService.getAllTasks();
                return Ok(result);
            }
            catch(Exception e)
            {
                return BadRequest("Some error occured " + e.Message);
            }
        }


        [HttpPost("todolist")]
        public  ActionResult create(task task)
        {
            try
            {
                _toDoListService.createTask(task);
                return Ok("Created task succesfully");
            }
            catch(Exception e)
            {
                return BadRequest("Some error occured " + e.Message);
            }
        }

        [HttpPut("todolist")]
        public  ActionResult updateStatus(int id, Status status)
        {
            try
            {
                _toDoListService.updateTask(id,status);
                return Ok("Updated task successfully");
            }
            catch (Exception e)
            {
                return BadRequest("Some error occured " + e.Message);
            }
        }


        [HttpDelete("todolist/{id}")]
        public async Task<ActionResult> deleteTask(int id)
        {
            try
            {
                _toDoListService.deleteTask(id);
                return Ok("Deleted task Successfully");
            }
            catch(Exception e)
            {
                return BadRequest("Some error occured " + e.Message);
            }
        }


    }
}

