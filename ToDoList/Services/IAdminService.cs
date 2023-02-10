using System;
using ToDoList.Model;

namespace ToDoList.Services
{
	public interface IAdminService
	{
        public void ApproveTask(int userId, int id);
        public List<TasksModel> GetAllTasks(int userId);
        public void UpdateTask(int id, Status Status, int userId);
        public List<UserRequesteModel> GetAllUsers(int userId);
        public void DeleteTask(int id, int userId);
    }
}

