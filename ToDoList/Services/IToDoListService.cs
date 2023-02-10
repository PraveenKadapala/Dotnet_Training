using System;
using ToDoList.Model;

namespace ToDoList.Services
{
    public interface IToDoListService
    {
        public TasksModel GetTaskById(int id);
        public List<TasksModel> GetUserTasks(int userId);
        public List<TasksModel> GetUserCompletedTasks(int userId);
        public void CreateTask(int userId, Model.Task task);
        public void UpdateTask(int id, int userId);
        public void DeleteTask(int id, int userId);
    }
}


