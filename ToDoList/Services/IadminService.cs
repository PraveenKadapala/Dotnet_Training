using System;
using ToDoList.Model;

namespace ToDoList.Services
{
	public interface IadminService
	{
        public void approveTask(int userId, int id);
        public List<tasksModel> getAllTasks(int userId);
    }
}

