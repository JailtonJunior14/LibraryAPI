using Library.Data.DTOs;

namespace Library.Services.LoanService
{
    public interface ILoanService
    {
        public Task<IEnumerable<LoanDTO>> GetAll();
        public Task<IEnumerable<LoanDTO>> GetByIdUser(Guid idUser);
        public Task<IEnumerable<LoanDTO>> GetByIdBook(Guid idBook);
        public Task<LoanDTO> GetById( Guid id);
        public Task<LoanDTO> Create(LoanInsertDTO dto);
        public Task<LoanUpdateDTO> Update(LoanUpdateDTO dto);

    }
}
