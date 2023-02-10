using System;
using Org.BouncyCastle.Asn1.Ocsp;
using ToDoList.Model;


namespace ToDoList.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ILogger<UserRepository> _logger;
        private readonly ToDoListDBContext _dbContext;
        public UserRepository(ILogger<UserRepository> logger, ToDoListDBContext toDoListDBContext)
        {
            _dbContext = toDoListDBContext;
            _logger = logger;

        }


        public void AddUser(User user)
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
        public User GetUser(loginRequestModel request)
        {
            try
            {
                _logger.LogInformation("Entering getUser repository");
                var user = _dbContext.Users.Where(u => u.Email == request.Email).FirstOrDefault();
                if (user == null)
                {
                    throw new Exception("User not found");
                }
                else
                {
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

