using Library.Data.Entities;
using Library.Data.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Library.Data.Repositorys.UserRepository
{
    public class UserRepository : IUserRepository
    {
        private readonly LibraryDbContext _context;
        public UserRepository(LibraryDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            var users = await _context.User.ToListAsync(); //passar para listar apenas os com is delete false

            return users;
        }
        public async Task<User?> GetById(Guid id)
        {
            var user = await _context.User.FindAsync(id);

            return user;
        }

        public async Task<bool> EmailExists(string email)
        {
            bool exists = await _context.User.AnyAsync(e => e.Email == email);

            return exists;
        }
        public async Task<User?> Login(string email)
        {
            var user = await _context.User.Where(e => e.Email == email).FirstAsync();

            return user;
        }
        public async Task<User> Create(User user)
        {
            await _context.AddAsync(user);
            await _context.SaveChangesAsync();
            return user;
        }
    }
}
