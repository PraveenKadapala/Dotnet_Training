using System;
using ToDoList.Model;

namespace ToDoList.Services
{
	public interface IuserService
	{
        public void addUser(userModel user);
        public String login(loginRequestModel request);

    }
}

