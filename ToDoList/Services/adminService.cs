using System;
using ToDoList.Controllers;
using ToDoList.Model;
using ToDoList.Repositories;

namespace ToDoList.Services
{
    public class adminService : IadminService
    {
        private readonly ItoDoListRepository _toDoListRepository;
        private readonly ILogger<adminService> _logger;
        public adminService(ILogger<adminService> logger, ItoDoListRepository toDoListRepository)
        {
            _toDoListRepository = toDoListRepository;
            _logger = logger;
        }
        public void approveTask(int userId, int id)
        {
            try
            {

                _logger.LogInformation("Entering approveTask service");
                var user = _toDoListRepository.getUser(userId);
                if (user.Admin == 1)
                {
                    var result = _toDoListRepository.getTaskById(id);
                    if (result == null)
                    {
                        throw new Exception("Task doesn't exist");
                    }
                    else if (result.Approval == 1)
                    {
                        throw new Exception("Task already approved");
                    }
                    else
                    {
                        result.Approval = 1;
                        _toDoListRepository.updateTask(result);
                    }
                }
                else
                {
                    throw new Exception("You are not authorized");
                }
            }
            catch
            {
                throw;
            }

        }

        public List<tasksModel> getAllTasks(int userId)
        {
            try
            {
                _logger.LogInformation("Entering getAllTasks service");
                var user = _toDoListRepository.getUser(userId);
                if (user.Admin == 1)
                {
                    var result = _toDoListRepository.getAllTasks();
                    var req_tasks = new List<tasksModel>();
                    result.ForEach(t =>
                    {
                        var task = new tasksModel();
                        task.Id = t.Id;
                        task.Task = t.Task;
                        task.Status = t.Status;
                        task.Approval = t.Approval;
                        task.UserId = t.UserId;
                        req_tasks.Add(task);
                    });
                    return (req_tasks);
                }
                else
                {
                    throw new Exception("You are not authorized");
                }
            }
            catch
            {
                throw;
            }
        }
    }
}

