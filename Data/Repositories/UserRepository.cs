using Dapper;
using Microsoft.Data.SqlClient;
using MyStore.Models;
using System.Data;

namespace MyStore.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DapperContext _context;
        private readonly string _connectionString;

        public UserRepository(DapperContext context)
        {
            _context = context;
        }

        public User? GetByUsername(string username)
        {
            using var connection = _context.CreateConnection();
            return connection.QueryFirstOrDefault<User>(
                "sp_GetUserByUsername",
                new { Username = username },
                commandType: CommandType.StoredProcedure
            );
        }

        //public void AddUser(User user)
        //{
        //    using var connection = _context.CreateConnection();
        //    connection.Execute(
        //        "sp_RegisterUser",
        //        new
        //        {
        //            user.Username,
        //            user.Password,
        //            user.Role
        //        },
        //        commandType: CommandType.StoredProcedure
        //    );
        //}


        public bool AddUser(User user)
        {
            using var connection = _context.CreateConnection();
            try
            {
                connection.Execute("SP_RegisterUser", new
                {
                    user.Username,
                    user.Password,
                    user.Role
                }, commandType: CommandType.StoredProcedure);

                return true;
            }
            catch
            {
                return false;
            }
        }

        public void UpdatePassword(string username, string newPassword)
        {
            using var connection = _context.CreateConnection();
            connection.Execute("sp_UpdatePassword", new
            {
                Username = username,
                NewPassword = newPassword
            }, commandType: CommandType.StoredProcedure);
        }
    }
}
