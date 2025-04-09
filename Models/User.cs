namespace MyStore.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; } // Plain for now
        public string Role { get; set; }
    }
}
