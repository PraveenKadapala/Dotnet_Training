using System;
using ToDoList.Model;

namespace ToDoList.Services
{
	public interface ItoDoListService
	{
        public List<ToDo> getTask(int id);
        public List<ToDo> getAllTasks();
        public void createTask(task task);
        public void updateTask(int id, Status toDo);
        public void deleteTask(int id);

    }
}


