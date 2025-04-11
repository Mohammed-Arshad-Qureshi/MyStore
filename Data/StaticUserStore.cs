using MyStore.Models;

namespace MyStore.Data
{
    public static class StaticUserStore
    {
        //public static List<User> Users = new()
        //{
        //    new User { Id = 1, Username = "admin", Password = "admin123", Role = "Admin" },
        //    new User { Id = 2, Username = "user", Password = "user123", Role = "Customer" }
        //};

        //public static List<User> Users = new List<User>
        //{
        //    new User
        //    {
        //        Username = "admin",
        //        Password = BCrypt.Net.BCrypt.HashPassword("admin123"),
        //        Role = "Admin"
        //    },
        //    new User
        //    {
        //        Username = "user",
        //        Password = BCrypt.Net.BCrypt.HashPassword("user123"),
        //        Role = "User"
        //    }
        //};
        //}

        public static List<User> Users { get; private set; } = new List<User>();

        public static void Initialize(IConfiguration config)
        {
            string pepper = config["PasswordPepper"] ?? "";

            Users = new List<User>
        {
            new User
            {
                Username = "admin",
                Password = BCrypt.Net.BCrypt.HashPassword("admin123" + pepper),
                Role = "Admin"
            },
            new User
            {
                Username = "user",
                Password = BCrypt.Net.BCrypt.HashPassword("user123" + pepper),
                Role = "User"
            }
        };
        }
    }
}
