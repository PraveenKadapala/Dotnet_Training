using System;
using ToDoList.Model;
using ToDoList.Utils;

namespace ToDoList.Repositories
{
	public class AdminRepository : IAdminRepository
	{
        private readonly ILogger<AdminRepository> _logger;
        private readonly ToDoListDBContext _dbContext;
        public AdminRepository(ILogger<AdminRepository> logger, ToDoListDBContext toDoListDBContext)
		{
			_dbContext = toDoListDBContext;
			_logger = logger;
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
        public List<User> GetAllUsers(int userId)
        {
            try
            {
                _logger.LogInformation("Entering getAllUsers repository");
                var users = _dbContext.Users.ToList();
                //var tasks = _dbContext.Users.Where(u => u.Id != userId).ToList();
                return users;
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
    }
}

