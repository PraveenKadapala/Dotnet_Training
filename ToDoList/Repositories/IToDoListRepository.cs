using System;
using ToDoList.Model;

namespace ToDoList.Repositories
{
    public interface IToDoListRepository
    {
        public ToDo GetTaskById(int id);
        public List<ToDo> GetAllTasks();
        public List<ToDo> GetUserTasks(int userID);
        public List<ToDo> GetUserCompletedTasks(int userID);
        public void CreateTask(ToDo toDo);
        public void UpdateTask(ToDo toDo);
        public void DeleteTask(ToDo toDo);
        public User GetUser(int id);
    }
}

