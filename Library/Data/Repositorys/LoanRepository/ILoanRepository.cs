using Library.Data.DTOs;
using Library.Data.Entities;

namespace Library.Data.Repositorys.LoanRepository
{
    public interface ILoanRepository
    {
        public Task<IEnumerable<Loan>> GetAll();
        public Task<Loan?> GetById(Guid id);
        public Task<IEnumerable<Loan>> GetByBookId(Guid id);
        public Task<IEnumerable<Loan>> GetByUserId(Guid id);
        public Task<IEnumerable<Loan>> GetByLibrarianId(Guid id);
        public Task<Loan> Create(Loan loan);
        public Task<Loan> Update(Loan loan);
        public Task<bool> Delete(Guid id);
    }
}
