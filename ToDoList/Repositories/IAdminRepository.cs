using System;
using ToDoList.Model;

namespace ToDoList.Repositories
{
	public interface IAdminRepository
	{
        public void UpdateTask(ToDo toDo);
        public List<User> GetAllUsers(int userId);
        public void DeleteTask(ToDo toDo);
    }
}

