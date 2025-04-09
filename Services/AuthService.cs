using MyStore.Data;
using MyStore.DTOs;
using MyStore.Helpers;
using MyStore.Interfaces;

namespace MyStore.Services
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _config;

        public AuthService(IConfiguration config)
        {
            _config = config;
        }

        public LoginResponse Authenticate(LoginRequest request)
        {
            var user = StaticUserStore.Users
                .FirstOrDefault(u => u.Username == request.Username && u.Password == request.Password);

            if (user == null) return null;

            var token = JwtTokenHelper.GenerateToken(user, _config["Jwt:Key"]);

            return new LoginResponse
            {
                Token = token,
                Username = user.Username,
                Role = user.Role
            };
        }
    }
}
