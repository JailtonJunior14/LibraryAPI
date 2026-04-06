using Library.Data.DTOs;
using Library.Data.Entities;
using Library.Data.Repositorys.BookRepository;
using Library.Data.Repositorys.LoanRepository;
using Library.Data.Repositorys.UserRepository;
using Library.Logs;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Primitives;
using System.Data.Common;
using System.Security.AccessControl;


namespace Library.Services.LoanService
{
    public class LoanService : ILoanService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserRepository _userRepository;
        private readonly IBookRepository _bookRepository;
        private readonly ILoanRepository _loanRepository;

        public LoanService(IHttpContextAccessor httpContextAccessor, IUserRepository userRepository, IBookRepository bookRepository, ILoanRepository loanRepository)
        {
            _httpContextAccessor = httpContextAccessor;
            _userRepository = userRepository;
            _bookRepository = bookRepository;
            _loanRepository = loanRepository;
        }
       
        public async Task<IEnumerable<LoanDTO>> GetAll()
        {
            try
            {
                var loans = await _loanRepository.GetAll();
                var books = await _bookRepository.GetAll();
                var book = books.Select(b => new BookDTO
                {
                    Author = b.Author,
                    ISBN = b.ISBN,
                    Publisher = b.Publisher,
                    Quantity = b.Quantity,
                    Title = b.Title,
                    YearPublication = b.YearPublication
                });

                return loans.Select(l => new LoanDTO
                {
                    Book = (BookDTO)book,
                    Status = l.Status,
                    Idlibrarian = l.Idlibrarian,
                    IdUser = l.IdUser,
                    DateCheckOut = l.DateCheckOut,
                    DateReturn = l.DateReturn
                });
            }catch (Exception ex)
            {
                Log.LogToFile("service GETALL loan", ex.GetType().ToString(), ex.Message);
                throw;
            }

        }

        public async Task<IEnumerable<LoanDTO>> GetByIdUser(Guid idUser)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<LoanDTO>> GetByIdBook(Guid idBook)
        {
            throw new NotImplementedException();

        }

        public async Task<LoanDTO> GetById(Guid id)
        {
            throw new NotImplementedException();

        }
        public async Task<LoanDTO> Create(LoanInsertDTO dto)
        {
            try
            {
                var librarian = _httpContextAccessor.HttpContext?.User?.FindFirst("UserID")?.Value;

                if (await _userRepository.GetById(dto.IdUser) == null)
                    throw new Exception("USUARIO NÃO ENCONTRADO");



                var loanuser = await _loanRepository.GetByUserId(dto.IdUser) ?? throw new Exception("Usuario não encontrado");

                if (loanuser.Where(l => l.Status != Enums.LoanRole.Returned).Any())
                    throw new Exception("Usuario com livro pendente!!");
                
                var book = await _bookRepository.GetById(dto.IdBook) ?? throw new Exception("Livro não encontrado");

                book.Quantity -= 1;
                await _bookRepository.Update(book);

                var loan = new Loan
                {
                    IdUser = dto.IdUser,
                    Idlibrarian = Guid.Parse(librarian),
                    IdBook = dto.IdBook,
                    DateCheckOut = dto.DateCheckOut,
                    DateReturn = dto.DateReturn,
                    Status = dto.Status,
                };

                await _loanRepository.Create(loan);

                return new LoanDTO
                {
                    Status = loan.Status,
                    DateCheckOut = loan.DateCheckOut,
                    DateReturn = loan.DateReturn,
                    Idlibrarian = loan.Idlibrarian,
                    IdUser = loan.IdUser,
                    Book = new BookDTO
                    {
                        Author = book.Author,
                        ISBN = book.ISBN,
                        Publisher = book.Publisher,
                        Quantity = book.Quantity,
                        Title = book.Title,
                        YearPublication = book.YearPublication
                    },
                };
            }catch(Exception ex)
            {
                
                Log.LogToFile("service create loan", ex.GetType().ToString(), ex.Message);
                throw;
            }

        }
    
        public async Task<LoanUpdateDTO> Update(LoanUpdateDTO dto)
        {
            try
            {
                var loan = await _loanRepository.GetById(dto.Id) ?? throw new Exception("Emprestimo não encontrado");

                if(dto.IdUser != null) loan.IdUser = (Guid)dto.IdUser;
                if (dto.IdBook != null) loan.IdBook = (Guid)dto.IdBook;
                if(dto.Status != null) loan.Status = (Enums.LoanRole)dto.Status;
                if(dto.DateCheckOut != null) loan.DateCheckOut = (DateTime)dto.DateCheckOut;
                if(dto.DateReturn != null) loan.DateReturn = (DateTime)dto.DateReturn;

                var loanF = await _loanRepository.Update(loan);

                return new LoanUpdateDTO
                {
                    Id = loanF.Id,
                    IdUser = loanF.IdUser,
                    IdBook = loanF.IdBook,
                    Status = loanF.Status,
                    DateCheckOut = loanF.DateCheckOut,
                    DateReturn = loanF.DateReturn,
                };

            }catch(Exception ex)
            {
                Log.LogToFile("service update loan", ex.GetType().ToString(), ex.Message);
                throw;
            }
        }
        
    }
}
