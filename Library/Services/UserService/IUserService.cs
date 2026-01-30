using Library.Data.DTOs;

namespace Library.Services.UserService
{
    public interface IUserService
    {
        public Task<IEnumerable<UserDTO>> GetAll();
        public Task<UserDTO?>GetById(Guid id);
        public Task<UserInsertDTO> Create(UserInsertDTO dto);
        
    }
}
