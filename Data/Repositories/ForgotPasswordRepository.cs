using Dapper;
using MyStore.Models;
using System.Data;

namespace MyStore.Data.Repositories
{
    public class ForgotPasswordRepository : IForgotPasswordRepository
    {
        private readonly DapperContext _context;

        public ForgotPasswordRepository(DapperContext context)
        {
            _context = context;
        }

        public void SaveResetToken(string username, string token, DateTime expiry)
        {
            using var connection = _context.CreateConnection();
            connection.Execute("sp_SaveResetToken", new
            {
                Username = username,
                Token = token,
                Expiry = expiry
            }, commandType: CommandType.StoredProcedure);
        }

        public User? GetUserByValidToken(string token)
        {
            using var connection = _context.CreateConnection();
            return connection.QueryFirstOrDefault<User>("sp_GetValidResetToken", new
            {
                Token = token
            }, commandType: CommandType.StoredProcedure);
        }

        public void MarkTokenAsUsed(string token)
        {
            using var connection = _context.CreateConnection();
            connection.Execute("sp_MarkTokenUsed", new
            {
                Token = token
            }, commandType: CommandType.StoredProcedure);
        }
    }

}
