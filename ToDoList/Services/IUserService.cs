using System;
using ToDoList.Model;

namespace ToDoList.Services
{
    public interface IUserService
    {
        public void AddUser(UserModel user);
        public String Login(loginRequestModel request);

    }
}

