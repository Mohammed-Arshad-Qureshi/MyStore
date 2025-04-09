using MyStore.DTOs;

namespace MyStore.Interfaces
{
    public interface IAuthService
    {
        LoginResponse Authenticate(LoginRequest request);
    }
}
