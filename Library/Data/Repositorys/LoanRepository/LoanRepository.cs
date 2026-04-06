using Library.Data.DTOs;
using Library.Data.Entities;
using Library.Data.Persistence;
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
            var loan = await _context.Loans.ToListAsync();

            return loan;
        }
        public async Task<Loan?> GetById(Guid id)
        {
            var loan = await _context.Loans.Where(l => l.Id == id).FirstOrDefaultAsync();
            return loan;

        }

        public async Task<IEnumerable<Loan>> GetByBookId(Guid id)
        {
            var loan = await _context.Loans.Where(l => l.IdBook == id).ToListAsync();
            return loan;
        }
        public async Task<IEnumerable<Loan>> GetByUserId(Guid id)
        {
            var loan = await _context.Loans.Where(l => l.IdUser == id).ToListAsync();
            return loan;
        }

        public async Task<Loan> Create(Loan loan)
        {
            await _context.AddAsync(loan);
            await _context.SaveChangesAsync();
            return loan;

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
