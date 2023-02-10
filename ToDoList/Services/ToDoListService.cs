using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ToDoList.Model;
using ToDoList.Repositories;
using ToDoList.Utils;
using ToDoList.Services;

namespace ToDoList.Services
{
    public class ToDoListService : IToDoListService
    {
        private readonly ILogger<ToDoListService> _logger;
        private readonly IToDoListRepository _toDoListRepository;
        private readonly ServiceBusSenderService _serviceBusSenderService;
        private readonly ServiceBusReceiverService _serviceBusReceiverService;
        
        public ToDoListService(ILogger<ToDoListService> logger, IToDoListRepository toDoListRepository)
        {
            _toDoListRepository = toDoListRepository;
            _logger = logger;
            //_serviceBusSenderService = serviceBusSenderService;
            //_serviceBusReceiverService = serviceBusReceiverService;

        }

        public TasksModel GetTaskById(int id)
        {
            try
            {
                _logger.LogInformation("Entering getTaskById service");
                var result = _toDoListRepository.GetTaskById(id);
                //_serviceBusReceiverService.Receiver();
                if (result == null)
                {
                    throw new Exception("Task doesn't exist");
                }
                else if (result.Approval == CommonUtils.PENDING)
                {
                    throw new Exception("Task not approved");
                }
                var task = new TasksModel();
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

        public List<TasksModel> GetUserTasks(int userId)
        {
            try
            {
                _logger.LogInformation("Entering getUserTasks service");
                var result = _toDoListRepository.GetUserTasks(userId);
                var req_tasks = new List<TasksModel>();

                result.ForEach(t =>
                {
                    var task = new TasksModel();
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
        public List<TasksModel> GetUserCompletedTasks(int userId)
        {
            try
            {
                _logger.LogInformation("Entering getUserCompletedTasks service");
                var result = _toDoListRepository.GetUserCompletedTasks(userId);
                Console.WriteLine("tasks:", result);
                var req_tasks = new List<TasksModel>();
                result.ForEach(t =>
                {
                    var task = new TasksModel();
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

        public void CreateTask(int userId, Model.Task task)
        {
            try
            {
                _logger.LogInformation("Entering createTask service");
                var user = _toDoListRepository.GetUser(userId);
                var username = user.Username;
                var toDo = new ToDo()
                {
                    Task = task.task,
                    Status = CommonUtils.PENDING,
                    Approval = CommonUtils.PENDING,
                    UserId = user.Id,

                };
                _toDoListRepository.CreateTask(toDo);
                //_serviceBusSenderService.Sender("User "+username+" created a task review it and approve");
            }
            catch
            {
                throw;
            }
        }

        public void UpdateTask(int id, int userId)
        {
            try
            {

                _logger.LogInformation("Entering updateTask service");
                var user = _toDoListRepository.GetUser(userId);
                var username = user.Username;
                var result = _toDoListRepository.GetTaskById(id);
                if (result == null)
                {
                    throw new Exception("Task doesn't exist");
                }
                else if (result.Approval == CommonUtils.PENDING)
                {
                    throw new Exception("Task is not approved by admin");
                }
                else if (result.Status == CommonUtils.REQUESTED)
                {
                    throw new Exception("Status already updated for Admin request");
                }
                else if (result.Status == CommonUtils.COMPLETED)
                {
                    throw new Exception("Status already approved by admin");
                }
                else
                {
                    result.Status = CommonUtils.REQUESTED;
                    _toDoListRepository.UpdateTask(result);
                }
                _toDoListRepository.UpdateTask(result);
                //_serviceBusSenderService.Sender("User " + username + "updated the " + result.Task + "task status to done review it");
            }
            catch
            {
                throw;
            }
        }
        public void DeleteTask(int id, int userId)
        {
            try
            {
                _logger.LogInformation("Entering deleteTask service");
                var user = _toDoListRepository.GetUser(userId);
                var username = user.Username;
                var result = _toDoListRepository.GetTaskById(id);
                if (result == null)
                {
                    throw new Exception("Task doesn't exist");
                }
                else if (result.Approval == CommonUtils.PENDING)
                {
                    throw new Exception("Task not approved");
                }
                else if (result.Status == CommonUtils.PENDING)
                {
                    throw new Exception("Can't delete the task since it is still pending");
                }

                else if (result.Status == CommonUtils.REQUESTED)
                {
                    throw new Exception("Can't delete the task since it is requested for admin approval");
                }
                _toDoListRepository.DeleteTask(result);
                //_serviceBusSenderService.Sender("User " + username + "deleted the " + result.Task + "task");
            }
            catch
            {
                throw;
            }
        }
    }
}

