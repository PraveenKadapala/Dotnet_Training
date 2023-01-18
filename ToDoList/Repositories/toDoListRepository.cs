using System;
using Microsoft.EntityFrameworkCore;
using ToDoList.Controllers;
using ToDoList.Model;

namespace ToDoList.Repositories
{
	public class toDoListRepository : ItoDoListRepository
	{
        private readonly ILogger<toDoListRepository> _logger;
        private readonly ToDoListDBContext _dbContext;
		public toDoListRepository(ILogger<toDoListRepository> logger, ToDoListDBContext toDoListDBContext)
		{
            _dbContext = toDoListDBContext;
            _logger = logger;

        }
        public ToDo getTaskById(int id)
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

        public List<ToDo> getUserTasks(int userID)
        {
            try
            {
                _logger.LogInformation("Entering getUserTasks repository");
                var tasks = _dbContext.ToDos.Where(t =>t.UserId == userID && t.Approval ==1).ToList();
                return tasks;
            }
            catch
            {
                throw;
            }
        }
        public List<ToDo> getAllTasks()
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

        public void createTask(ToDo toDo)
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

        public void updateTask(ToDo toDo)
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

        public void deleteTask(ToDo toDo)
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
        public User getUser(int id)
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
