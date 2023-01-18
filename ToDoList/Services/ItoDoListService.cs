using System;
using ToDoList.Model;

namespace ToDoList.Services
{
	public interface ItoDoListService
	{
        public tasksModel getTaskById(int id);
        public List<tasksModel> getUserTasks(int userId);
        public void createTask(int userId,task task);
        public void updateTask(int id);
        public void deleteTask(int id);
    }
}


