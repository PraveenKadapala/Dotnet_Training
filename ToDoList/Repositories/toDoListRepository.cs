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
        public List<ToDo> getTaskById(int id)
        {
            try
            {
                //var task =  _dbContext.ToDos.FindAsync(id);
                var task = _dbContext.ToDos.Where(t => t.Id == id).ToList();
                _logger.LogWarning("Task:", task);
                return task;
            }
            catch(Exception e)
            {
                throw new("Some error occured " + e.Message);
            }
        }

        public List<ToDo> getAllTasks()
        {
            try
            {
                var tasks = _dbContext.ToDos.ToList();
                return tasks;
            }
            catch(Exception e)
            {
                throw new("Some error occured " + e.Message);
            }
        }

        public void createTask(ToDo toDo)
        {
            try
            {
                _dbContext.ToDos.Add(toDo);
                _dbContext.SaveChanges();
            }
            catch (Exception e)
            {
                throw new("Some error occured " + e.Message);
            }
        }

        public void updateTask(int id, Status status)
        {
            try
            {
               
                var result = _dbContext.ToDos.Where(t => t.Id == id).First();
                if (result == null)
                {
                    throw new("Task doesn't exist");
                }
                result.Status = status.status;
                _dbContext.ToDos.Update(result);
                _dbContext.SaveChanges();
            }
            catch (Exception e)
            {
                throw new("Some error occured " + e.Message);
            }

}

        public void deleteTask(int id)
        {
            try
            {
                var result = _dbContext.ToDos.Where(t => t.Id == id).First();
                if (result == null)
                {
                    throw new("Task doesn't exist");
                }
                _dbContext.ToDos.Remove(result);
                _dbContext.SaveChanges();
            }
            catch (Exception e)
            {
                throw new("Some error occured " + e.Message);
            }

        }
    }
}
