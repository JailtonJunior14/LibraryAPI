using Library.Data.DTOs;

namespace Library.Services.AuthService
{
    public interface IAuthService
    {
        public Task<LoginResponseDTO> Login(string email, string password);
    }
}
