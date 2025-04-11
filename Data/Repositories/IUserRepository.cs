using MyStore.Models;

namespace MyStore.Data.Repositories
{
    public interface IUserRepository
    {

        User? GetByUsername(string username);
        bool AddUser(User user);
        void UpdatePassword(string username, string newPassword);
    }
}
