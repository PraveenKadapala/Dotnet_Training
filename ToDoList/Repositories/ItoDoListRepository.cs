using System;
using ToDoList.Model;

namespace ToDoList.Repositories
{
	public interface ItoDoListRepository
	{
        public ToDo getTaskById(int id);
        public List<ToDo> getAllTasks();
        public List<ToDo> getUserTasks(int userID);
        public void createTask(ToDo toDo);
        public void updateTask(ToDo toDo);
        public void deleteTask(ToDo toDo);
        public User getUser(int id);
    }
}

