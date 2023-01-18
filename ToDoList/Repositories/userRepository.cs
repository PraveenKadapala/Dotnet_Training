using System;
using Org.BouncyCastle.Asn1.Ocsp;
using ToDoList.Model;


namespace ToDoList.Repositories
{
	public class userRepository : IuserRepository
	{
        private readonly ILogger<userRepository> _logger;
        private readonly ToDoListDBContext _dbContext;
        public userRepository(ILogger<userRepository> logger, ToDoListDBContext toDoListDBContext)
        {
            _dbContext = toDoListDBContext;
            _logger = logger;

        }


        public void addUser(User user)
        {
            try
            {
                _logger.LogInformation("Entering addUser repository");
                _dbContext.Users.Add(user);
                _dbContext.SaveChanges();
            }
            catch
            {
                throw;
            }
        }
        public User getUser(loginRequestModel request)
        {
            try
            {
                _logger.LogInformation("Entering getUser repository");
                var user = _dbContext.Users.Where(u => u.Email == request.Email).FirstOrDefault();
                if (user == null)
                {
                    throw new Exception("User not found");
                }
                else {
                    return user;
                }
            }
            catch
            {
                throw;
            }
        }
    }
}

