using Library.Data.DTOs;
using Library.Data.Entities;
using Library.Data.Repositorys.UserRepository;
using Library.Logs;
using Library.Services.Helpers.PasswordHelper;

namespace Library.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly IPasswordHelper _passwordHelper;
        private readonly IUserRepository _userRepository;
        public UserService(IPasswordHelper passwordHelper, IUserRepository userRepository)
        {
            _passwordHelper = passwordHelper;
            _userRepository = userRepository;
        }
        public async Task<IEnumerable<UserDTO>> GetAll()
        {
            var usersDTO = await _userRepository.GetAll();

            return usersDTO.Select(u => new UserDTO
            {
                Role = u.Role,
                Email = u.Email,
                Name = u.UserName
            }).ToList();
        }
        public async Task<UserDTO?> GetById(Guid id)
        {
            var userDTO =await _userRepository.GetById(id);

            if (userDTO == null)
                throw new ApplicationException("Usuario não encontrado");

            return new UserDTO
            {
                Email = userDTO.Email,
                Name = userDTO.UserName
            };
        }
        public async Task<UserInsertDTO> Create(UserInsertDTO dto)
        {
            try
            {

                var userPass = new User();
                var encryptedPassword = _passwordHelper.HashPassWord(userPass, dto.Password);

                var existsemail = await _userRepository.EmailExists(dto.Email);

                if (existsemail)
                    throw new ApplicationException("Email ja existe");

                if(dto.Password.Length < 6 )
                    throw new ApplicationException("Senha deve ter no minimo 6 digitos");

                if (dto.Password != dto.PasswordConfirmation)
                    throw new ApplicationException("Senhas não coincidem");
                    
                var user = new User
                {
                    Role = dto.Role,
                    Email = dto.Email,
                    UserName = dto.Name,
                    PasswordHash = encryptedPassword
                };

                var userF = await _userRepository.Create(user);

                Log.LogToFile("Cadastro", "sucesso");

                return new UserInsertDTO
                {
                    Role = userF.Role,
                    Email = userF.Email,
                    Name = userF.UserName,
                    Password = userF.PasswordHash
                };

            }
            catch (Exception ex)
            {
                Log.LogToFile("Cadastro erro", ex.Message);

                throw;
            }
        }
        

    }
}
