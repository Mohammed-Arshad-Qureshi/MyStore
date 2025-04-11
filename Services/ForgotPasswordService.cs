using MyStore.Data.Repositories;
using MyStore.Interfaces;

namespace MyStore.Services
{
    public class ForgotPasswordService : IForgotPasswordService
    {
        private readonly IForgotPasswordRepository _resetRepo;
        private readonly IUserRepository _userRepo;
        private readonly IConfiguration _config;

        public ForgotPasswordService(
            IForgotPasswordRepository resetRepo,
            IUserRepository userRepo,
            IConfiguration config)
        {
            _resetRepo = resetRepo;
            _userRepo = userRepo;
            _config = config;
        }

        public string GenerateResetToken(string username)
        {
            var user = _userRepo.GetByUsername(username);
            if (user == null)
                return "User not found";

            var token = Guid.NewGuid().ToString();
            var expiry = DateTime.UtcNow.AddMinutes(15);

            _resetRepo.SaveResetToken(username, token, expiry);

            // In real apps, send email here. For now, just return the reset link.
            var frontendUrl = _config["App:FrontendUrl"]; // e.g., http://localhost:4200
            var resetLink = $"{frontendUrl}/reset-password?token={token}";
            return resetLink;
        }

        public string ResetPassword(string token, string newPassword)
        {
            var user = _resetRepo.GetUserByValidToken(token);
            if (user == null)
                return "Invalid or expired token";

            var pepper = _config["PasswordPepper"];
            var hashed = BCrypt.Net.BCrypt.HashPassword(newPassword + pepper);

            user.Password = hashed;
            _userRepo.UpdatePassword(user.Username, hashed);
            _resetRepo.MarkTokenAsUsed(token);

            return "Password reset successful";
        }
    }

}
