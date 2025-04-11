namespace MyStore.Interfaces
{
    public interface IForgotPasswordService
    {
        string GenerateResetToken(string username);
        string ResetPassword(string token, string newPassword);
    }
}
