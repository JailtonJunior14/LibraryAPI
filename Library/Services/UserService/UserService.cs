using Library.Data.DTOs;
using Library.Data.Entities;
using Library.Data.Repositorys.UserRepository;
using Library.Logs;
using Library.Services.Helpers.PasswordHelper;
using Microsoft.Extensions.Configuration.UserSecrets;
using System.ComponentModel;

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
            try
            {
                var usersDTO = await _userRepository.GetAll();

                return usersDTO.Select(u => new UserDTO
                {
                    Id = u.Id,
                    Role = u.Role,
                    Email = u.Email,
                    Name = u.UserName,
                    
                }).ToList();
            }
            catch (Exception ex)
            {
                Log.LogToFile("erro get all user", ex.Message);
                throw;
            }
            
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
        public async Task<UserDTO> Create(UserInsertDTO dto)
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

                return new UserDTO
                {
                    Id = userF.Id,
                    Role = userF.Role,
                    Email = userF.Email,
                    Name = userF.UserName
                };

            }
            catch (Exception ex)
            {
                Log.LogToFile("Cadastro erro", ex.Message);

                throw;
            }
        }
        
        public async Task<UserUpdateDTO> Update(UserUpdateDTO dto)
        {

            try
            {
                var user = await _userRepository.GetById(dto.Id) ?? throw new Exception("Usuario não encontrado");
                if (await _userRepository.EmailExists(dto.Email))
                    throw new Exception("Email já cadastrado");

                if (dto.Email != null) user.Email = dto.Email;
                if (dto.Name != null) user.UserName = dto.Name;
                if(dto.IsDeleted != null) user.IsDeleted = dto.IsDeleted;


                var userF = await _userRepository.Update(user);

                return new UserUpdateDTO
                {
                    Id = userF.Id,
                    Email = userF.Email,
                    Name = userF.UserName,
                    IsDeleted = dto.IsDeleted
                };
            }catch (Exception ex)
            {
                Log.LogToFile("update user error: ", ex.Message);
                throw;
            }

        }

        public async Task<bool> Delete(Guid id)
        {
            try
            {
                var user = await _userRepository.GetById(id) ?? throw new Exception("Usuario não encontrado");

                user.IsDeleted = true;

                await _userRepository.Delete(id);

                return true;

            }catch(Exception ex)
            {
                Log.LogToFile("Delete user erro: ", ex.Message);
                throw;
            }
        }
    }
}
