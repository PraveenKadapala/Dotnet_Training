using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ToDoList.Model;
using ToDoList.Repositories;

namespace ToDoList.Services
{
	public class toDoListService : ItoDoListService
	{
        private readonly ILogger<toDoListService> _logger;
        private readonly ItoDoListRepository _toDoListRepository;
		public toDoListService(ILogger<toDoListService> logger, ItoDoListRepository toDoListRepository)
		{
			_toDoListRepository = toDoListRepository;
            _logger = logger;
        }

		public tasksModel getTaskById(int id)
		{
            try
            {
                _logger.LogInformation("Entering getTaskById service");
                var result  = _toDoListRepository.getTaskById(id);
                if(result == null)
                {
                    throw new Exception("Task doesn't exist");
                }else if(result.Approval != 1)
                {
                    throw new Exception("Task not approved");
                }
                var task = new tasksModel();
                task.Id = result.Id;
                task.Task = result.Task;
                task.Status = result.Status;
                task.Approval = result.Approval;
                task.UserId = result.UserId;
                return task;
            }
            catch
            {
                throw;
            }
        }

        public List<tasksModel> getUserTasks(int userId)
        {
            try
            {
                _logger.LogInformation("Entering getUserTasks service");
                var result= _toDoListRepository.getUserTasks(userId);
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
            catch
            {
                throw;
            }
        }

		public void createTask(int userId,task task)
		{
            try
            {
                _logger.LogInformation("Entering createTask service");
                var user = _toDoListRepository.getUser(userId);
                var toDo = new ToDo()
                {
                    Task = task.Task,
                    UserId = user.Id,
              
                };
                _toDoListRepository.createTask(toDo);
            }
            catch
            {
                throw;
            }
        }

        public void updateTask(int id)
        {
            try
            {
                
                _logger.LogInformation("Entering updateTask service");
                var result = _toDoListRepository.getTaskById(id);
                if (result == null)
                {
                    throw new Exception("Task doesn't exist");
                }
                else if (result.Status == 1)
                {
                    throw new Exception("Status already updated");
                }
                else
                {
                    result.Status = 1;
                    _toDoListRepository.updateTask(result);
                }
                _toDoListRepository.updateTask(result);
            }
            catch 
            {
                throw;
            }
        }
        public void deleteTask(int id)
        {
            try
            {
                _logger.LogInformation("Entering deleteTask service");
                var result = _toDoListRepository.getTaskById(id);
                if (result == null)
                {
                    throw new Exception("Task doesn't exist");
                }
                _toDoListRepository.deleteTask(result);
            }
            catch
            {
                throw;
            }
        }
    }
}

