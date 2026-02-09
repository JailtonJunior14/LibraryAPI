using Library.Data.DTOs;
using Library.Data.Repositorys.UserRepository;
using Library.Services.Helpers.PasswordHelper;
using Library.Logs;


namespace Library.Services.AuthService
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHelper _passwordHelper;
        public AuthService(IUserRepository userRepository, IPasswordHelper passwordHelper)
        {
            _userRepository = userRepository;
            _passwordHelper = passwordHelper;
        }
        public async Task<LoginResponseDTO> Login(string email, string password)
        {
            try
            {
                var user = await _userRepository.Login(email) ?? throw new Exception("usuario inexistente");

                var verification = _passwordHelper.VerifyPassword(user, user.PasswordHash, password);

                if (verification)
                {
                    return new LoginResponseDTO
                    {
                        Email = email,
                        Role = user.Role,
                    };
                }
                else
                {
                    throw new UnauthorizedAccessException("Email ou senha incorretos");
                }
            }
            catch (ApplicationException ex)
            {
                Log.LogToFile("ApplicationException auth", ex.Message);
                throw;
            }
            catch (Exception ex)
            {
                Log.LogToFile("Exception auth", ex.Message);
                throw;
            }
        }
    }
}
