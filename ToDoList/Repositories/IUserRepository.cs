using System;
using ToDoList.Model;

namespace ToDoList.Repositories
{
    public interface IUserRepository
    {
        public void AddUser(User user);
        public User GetUser(loginRequestModel request);

    }
}

