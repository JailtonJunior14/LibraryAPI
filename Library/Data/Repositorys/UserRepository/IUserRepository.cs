using Library.Data.Entities;

namespace Library.Data.Repositorys.UserRepository
{
    public interface IUserRepository
    {
        public Task<IEnumerable<User>> GetAll();
        public Task<User?> GetById(Guid id);
        public Task<bool> EmailExists(string email);
        public Task<User?> Login(string email);
        public Task<User> Create(User user);
    }
}
