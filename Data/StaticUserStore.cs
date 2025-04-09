using MyStore.Models;

namespace MyStore.Data
{
    public static class StaticUserStore
    {
        public static List<User> Users = new()
        {
            new User { Id = 1, Username = "admin", Password = "admin123", Role = "Admin" },
            new User { Id = 2, Username = "user", Password = "user123", Role = "Customer" }
        };
    }
}
