using Library.Data.DTOs;
using Library.Data.Entities;
using Library.Data.Persistence;
using Library.Logs;
using Microsoft.EntityFrameworkCore;

namespace Library.Data.Repositorys.LoanRepository
{
    public class LoanRepository : ILoanRepository
    {
        private readonly LibraryDbContext _context;
        public LoanRepository(LibraryDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Loan>> GetAll()
        {
            var loan = await _context.Loans.Include(l => l.Book).Include(l => l.Librarian).Include(l => l.User).ToListAsync();

            return loan;
        }
        public async Task<Loan?> GetById(Guid id)
        {
            var loan = await _context.Loans.Include(l => l.Book).Include(l => l.Librarian).Include(l => l.User).FirstOrDefaultAsync(l => l.Id == id);
            return loan;

        }

        public async Task<IEnumerable<Loan>> GetByBookId(Guid id)
        {
            var loan = await _context.Loans.Include(l => l.Librarian).Include(l => l.User).Include(l => l.Book).Where(l => l.BookId == id).ToListAsync();
            return loan;
        }
        public async Task<IEnumerable<Loan>> GetByUserId(Guid id)
        {
            var loan = await _context.Loans.Include(l => l.Book).Include(l => l.Librarian).Include(l => l.User).Where(l => l.UserId == id).ToListAsync();
            return loan;
        }

        public async Task<IEnumerable<Loan>> GetByLibrarianId(Guid id)
        {
            var loan = await _context.Loans.Include(l => l.Book).Include(l => l.User).Include(l => l.Librarian).Where(l => l.LibrarianId == id).ToListAsync();
            return loan;
        }

        public async Task<Loan> Create(Loan loan)
        {
            try
            {
                await _context.AddAsync(loan);
                await _context.SaveChangesAsync();
                return loan;
            }catch (DbUpdateException ex)
            {
                Log.LogToFile("erro repositorry create LOAN", ex.GetType().ToString(), ex.InnerException.Message);
                throw;
            }

        }

        public async Task<Loan> Update(Loan loan)
        {
            await _context.SaveChangesAsync();
            return loan;
        }
        public async Task<bool> Delete(Guid id)
        {
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
