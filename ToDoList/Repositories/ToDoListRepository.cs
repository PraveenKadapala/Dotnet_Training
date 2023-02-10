using System;
using Microsoft.EntityFrameworkCore;
using ToDoList.Controllers;
using ToDoList.Model;
using ToDoList.Utils;

namespace ToDoList.Repositories
{
    public class ToDoListRepository : IToDoListRepository
    {
        private readonly ILogger<ToDoListRepository> _logger;
        private readonly ToDoListDBContext _dbContext;
        public ToDoListRepository(ILogger<ToDoListRepository> logger, ToDoListDBContext toDoListDBContext)
        {
            _dbContext = toDoListDBContext;
            _logger = logger;

        }
        public ToDo GetTaskById(int id)
        {
            try
            {
                _logger.LogInformation("Entering getTaskById repository");
                var task = _dbContext.ToDos.Where(t => t.Id == id).FirstOrDefault();
                return task;
            }
            catch
            {
                throw;
            }
        }

        public List<ToDo> GetUserTasks(int userID)
        {
            try
            {
                _logger.LogInformation("Entering getUserTasks repository");
                var tasks = _dbContext.ToDos.Where(t => t.UserId == userID && t.Approval == CommonUtils.APPROVED && t.Status != CommonUtils.COMPLETED).ToList();
                return tasks;
            }
            catch
            {
                throw;
            }
        }
        public List<ToDo> GetUserCompletedTasks(int userID)
        {
            try
            {
                _logger.LogInformation("Entering getUserCompletedTasks repository");
                var tasks = _dbContext.ToDos.Where(t => t.UserId == userID && t.Approval == CommonUtils.APPROVED && t.Status == CommonUtils.COMPLETED).ToList();
                return tasks;
            }
            catch
            {
                throw;
            }
        }
        public List<ToDo> GetAllTasks()
        {
            try
            {
                _logger.LogInformation("Entering getAllTasks repository");
                var tasks = _dbContext.ToDos.ToList();
                return tasks;
            }
            catch
            {
                throw;
            }
        }

        public void CreateTask(ToDo toDo)
        {
            try
            {
                _logger.LogInformation("Entering createTask repository");
                _dbContext.ToDos.Add(toDo);
                _dbContext.SaveChanges();
            }
            catch
            {
                throw;
            }
        }

        public void UpdateTask(ToDo toDo)
        {
            try
            {
                _logger.LogInformation("Entering updateTask repository");
                _dbContext.ToDos.Update(toDo);
                _dbContext.SaveChanges();
            }
            catch
            {
                throw;
            }

        }

        public void DeleteTask(ToDo toDo)
        {
            try
            {
                _logger.LogInformation("Entering deleteTask repository");
                _dbContext.ToDos.Remove(toDo);
                _dbContext.SaveChanges();
            }
            catch
            {
                throw;
            }

        }
        public User GetUser(int id)
        {
            try
            {
                _logger.LogInformation("Entering getUser repository");
                return _dbContext.Users.Where(u => u.Id == id).First();
            }
            catch
            {
                throw;
            }
        }
    }
}
