using System;
using ToDoList.Model;

namespace ToDoList.Repositories
{
	public interface IuserRepository
	{
        public void addUser(User user);
        public User getUser(loginRequestModel request);

    }
}

