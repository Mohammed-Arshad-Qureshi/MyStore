using MyStore.Models;

namespace MyStore.Data.Repositories
{
    public interface IForgotPasswordRepository
    {
        void SaveResetToken(string username, string token, DateTime expiry);
        User? GetUserByValidToken(string token);
        void MarkTokenAsUsed(string token);
    }
}
