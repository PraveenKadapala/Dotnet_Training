using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ToDoList.Model;
using ToDoList.Repositories;

namespace ToDoList.Services
{
	public class toDoListService : ItoDoListService
	{

		private readonly ItoDoListRepository _toDoListRepository;
        private ToDo toDo;
		public toDoListService( ItoDoListRepository toDoListRepository)
		{
			_toDoListRepository = toDoListRepository;
		}

		public List<ToDo> getTask(int id)
		{
            try
            {
                return _toDoListRepository.getTaskById(id);
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
                return _toDoListRepository.getAllTasks();
            }
            catch(Exception e)
            {
                throw new("Some error occured " + e.Message);
            }
        }

		public void createTask(task task)
		{
            try
            {
                var toDo = new ToDo()
                {
                    Task = task.Task,
                    Status = 0
                };
                _toDoListRepository.createTask(toDo);
            }
            catch (Exception e)
            {
                throw new("Some error occured " + e.Message);
            }
        }

        public void updateTask(int id,Status status)
        {
            try
            {
                var toDo = new ToDo()
                {
                    Id = id,
                    Task = "",
                    Status = status.status
                };
                _toDoListRepository.updateTask(id, status);
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
                _toDoListRepository.deleteTask(id);
            }
            catch (Exception e)
            {
                throw new("Some error occured " + e.Message);
            }
        }
    }
}

