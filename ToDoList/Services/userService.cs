using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ToDoList.Model;
using ToDoList.Repositories;

namespace ToDoList.Services
{
    public class userService : IuserService
    {

        private readonly ILogger<toDoListService> _logger;
        private readonly IuserRepository _userRepository;
        private readonly IConfiguration _configuration;
        public userService(ILogger<toDoListService> logger, IuserRepository userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _logger = logger;
            _configuration = configuration;
        }
        public void addUser(userModel user)
        {
            try
            {
                _logger.LogInformation("Entering addUser service");
                var User = new User()
                {
                    Username = user.Username,
                    Password = user.Password,
                    Email = user.Email
                };
                _userRepository.addUser(User);
            }
            catch
            {
                throw;
            }
        }
        public String login(loginRequestModel request)
        {
            try
            {
                _logger.LogInformation("Entering login service");
                var user = _userRepository.getUser(request);
                if (user != null && user?.Password == request.Password)
                {
                    var token = createToken(user);
                    return token;
                }
                else
                {
                    throw new Exception("Invalid Username or Password");
                }
            }
            catch
            {
                throw;
            }
        }
        private string createToken(User user)
        {
            _logger.LogInformation("Entering createToken service");
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name,user.Id.ToString()),
                new Claim(ClaimTypes.Email,user.Email)
            };
            var tokenKey = _configuration.GetValue<string>("Token:Key");

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(tokenKey));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddMinutes(60),
                signingCredentials: cred
                );

            var Jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return Jwt;
        }

    }
}

