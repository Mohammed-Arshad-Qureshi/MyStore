using Dapper;
using Microsoft.Data.SqlClient;
using MyStore.Data;
using MyStore.Data.Repositories;
using MyStore.DTOs;
using MyStore.Helpers;
using MyStore.Interfaces;
using MyStore.Models;
using System.Data;

namespace MyStore.Services
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _config;
        private readonly IUserRepository _userRepo;

        public AuthService(IConfiguration config, IUserRepository userRepo)
        {
            _config = config;
            _userRepo = userRepo;
        }

        public LoginResponse? Authenticate(LoginRequest request)
        {
            var pepper = _config["PasswordPepper"];
            var user = _userRepo.GetByUsername(request.Username);

            if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password + pepper, user.Password))
                return null;

            var token = JwtTokenHelper.GenerateToken(user, _config["Jwt:Key"]??"");

            return new LoginResponse
            {
                Username = user.Username,
                Role = user.Role,
                Token = token
            };
        }

        //public string Register(User user, string rawPassword)
        //{
        //    var pepper = _config["PasswordPepper"];
        //    var existing = _userRepo.GetByUsername(user.Username);
        //    if (existing != null)
        //        return "Username already exists";

        //    user.Password = BCrypt.Net.BCrypt.HashPassword(rawPassword + pepper);
        //    _userRepo.AddUser(user);

        //    return "User registered successfully";
        //}

        public bool Register(RegisterRequest request)
        {
            var pepper = _config["PasswordPepper"]; // from appsettings.json
            var existingUser = _userRepo.GetByUsername(request.Username);
            if (existingUser != null)
                return false;

            var user = new User
            {
                Username = request.Username,
                Password = BCrypt.Net.BCrypt.HashPassword(request.Password + pepper),
                Role = request.Role
            };

            return _userRepo.AddUser(user);
        }

    }
}
