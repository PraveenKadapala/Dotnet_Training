using System;
using ToDoList.Controllers;
using ToDoList.Model;
using ToDoList.Repositories;
using ToDoList.Utils;

namespace ToDoList.Services
{
    public class AdminService : IAdminService
    {
        private readonly IToDoListRepository _toDoListRepository;
        private readonly ILogger<AdminService> _logger;
        private readonly IAdminRepository _adminRepository;
        public AdminService(ILogger<AdminService> logger, IToDoListRepository toDoListRepository, IAdminRepository adminRepository)
        {
            _toDoListRepository = toDoListRepository;
            _logger = logger;
            _adminRepository = adminRepository;
        }
        public void ApproveTask(int userId, int id)
        {
            try
            {

                _logger.LogInformation("Entering approveTask service");
                var user = _toDoListRepository.GetUser(userId);
                if (user.Role == CommonUtils.ADMIN)
                {
                    var result = _toDoListRepository.GetTaskById(id);
                    if (result == null)
                    {
                        throw new Exception("Task doesn't exist");
                    }
                    else if (result.Approval == CommonUtils.APPROVED)
                    {
                        throw new Exception("Task already approved by admin");
                    }
                    else
                    {
                        result.Approval = CommonUtils.APPROVED;
                        _toDoListRepository.UpdateTask(result);
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
        

        public List<TasksModel> GetAllTasks(int userId)
        {
            try
            {
                _logger.LogInformation("Entering getAllTasks service");
                var user = _toDoListRepository.GetUser(userId);
                if (user.Role == CommonUtils.ADMIN)
                {
                    var result = _toDoListRepository.GetAllTasks();
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
        public void UpdateTask(int id, Status status, int userId)
        {
            try
            {
                _logger.LogInformation("Entering updateTask service");
                var user = _toDoListRepository.GetUser(userId);
                if (user.Role == CommonUtils.ADMIN)
                {
                    var result = _toDoListRepository.GetTaskById(id);
                    if (result == null)
                    {
                        throw new Exception("Task doesn't exist");
                    }
                    else if (result.Status == CommonUtils.PENDING)
                    {
                        throw new Exception("Task is still pending");
                    }
                    else if (result.Status == CommonUtils.COMPLETED)
                    {
                        throw new Exception("Task already approved by admin");
                    }
                    else if (result.Status == CommonUtils.REQUESTED)
                    {
                        result.Status = status.status;
                        _adminRepository.UpdateTask(result);
                    }
                    _toDoListRepository.UpdateTask(result);
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
        public List<UserRequesteModel> GetAllUsers(int userId)
        {
            try
            {
                _logger.LogInformation("Entering updateTask service");
                var user = _toDoListRepository.GetUser(userId);
                if (user.Role == CommonUtils.ADMIN)
                {
                    var result = _adminRepository.GetAllUsers(userId);
                    var req_users = new List<UserRequesteModel>();
                    result.ForEach(u =>
                    {
                        var user = new UserRequesteModel();
                        user.Id = u.Id;
                        user.Username = u.Username;
                        user.Email = u.Email;
                        user.Role = u.Role;
                        req_users.Add(user);
                    });
                    return (req_users);
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
        public void DeleteTask(int id, int userId)
        {
            try
            {
                _logger.LogInformation("Entering deleteTask service");
                var user = _toDoListRepository.GetUser(userId);
                if (user.Role == CommonUtils.ADMIN)
                {
                    var result = _toDoListRepository.GetTaskById(id);
                    if (result == null)
                    {
                        throw new Exception("Task doesn't exist");
                    }
                    _toDoListRepository.DeleteTask(result);
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

