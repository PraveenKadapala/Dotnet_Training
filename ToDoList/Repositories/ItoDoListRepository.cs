using System;
using ToDoList.Model;

namespace ToDoList.Repositories
{
	public interface ItoDoListRepository
	{
        public List<ToDo> getTaskById(int id);
        public List<ToDo> getAllTasks();
        public void createTask(ToDo toDo);
        public void updateTask(int id,Status status);
        public void deleteTask(int id);
    }
}

