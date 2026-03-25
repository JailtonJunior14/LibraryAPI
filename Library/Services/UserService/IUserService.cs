using Library.Data.DTOs;

namespace Library.Services.UserService
{
    public interface IUserService
    {
        public Task<IEnumerable<UserDTO>> GetAll();
        public Task<UserDTO?>GetById(Guid id);
        public Task<UserDTO> Create(UserInsertDTO dto);
        public Task<UserUpdateDTO> Update(UserUpdateDTO dTO);

        public Task<bool> Delete(Guid id);
    }
}
