using Library.Data.DTOs;
using Library.Data.Entities;
using Library.Data.Repositorys.BookRepository;
using Library.Logs;
using System.Data.Common;
using System.Reflection.Metadata.Ecma335;

namespace Library.Services.BookService
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;

        public BookService(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task<IEnumerable<BookDTO>> GetAll()
        {
            try
            {
                var book = await _bookRepository.GetAll();

                return book.Select(x => new BookDTO
                {
                    Title = x.Title,
                    Author = x.Author,
                    Publisher = x.Publisher,
                    YearPublication = x.YearPublication,
                    ISBN = x.ISBN,
                    Quantity = x.Quantity,
                });
            }
            
            catch (Exception ex)
            {
                Log.LogToFile("Getall bookservice", ex.Message);
                throw;
            }
        }

        public async Task<BookDTO> GetById(Guid Id)
        {
            try
            {
                var book = await _bookRepository.GetById(Id);
                return new BookDTO
                {
                    Title = book.Title,
                    Author = book.Author,
                    Publisher = book.Publisher,
                    YearPublication = book.YearPublication,
                    ISBN = book.ISBN,
                    Quantity = book.Quantity,
                };
            }
            catch (Exception ex)
            {
                Log.LogToFile("getbyid bookservice", ex.Message);
                throw;
            }
        }

        public async Task<BookInsertDTO> Create(BookInsertDTO dto)
        {
            try
            {
                var isbnexists = await _bookRepository.ExistsISBN(dto.ISBN);
                if (isbnexists)
                {
                    throw new ApplicationException("ISBN já cadastrado.");
                }

                var book = new Book
                {
                    Author = dto.Author,
                    Title = dto.Title,
                    Publisher = dto.Publisher,
                    YearPublication = dto.YearPublication,
                    ISBN = dto.ISBN,
                    Quantity = dto.Quantity,
                };

                var bookf = await _bookRepository.Create(book);

                return new BookInsertDTO
                {
                    Author = bookf.Author,
                    Title = bookf.Title,
                    Publisher = bookf.Publisher,
                    ISBN = bookf.ISBN,
                    Quantity = bookf.Quantity,
                    YearPublication = bookf.YearPublication
                };
            }
            catch (DbException ex)
            {
                Log.LogToFile("Getall bookservice DbException", ex.Message);
                throw;
            }
            catch (Exception ex)
            {
                Log.LogToFile("create bookservice", ex.Message);
                throw;
            }
        }

        public async Task<BookUpdateDTO> Update(BookUpdateDTO dto)
        {
            try
            {
                var bookU = await _bookRepository.GetById(dto.Id) ?? throw new Exception("Livro não encontrado");

                if (dto.Author != null) bookU.Author = dto.Author;
                if (dto.Publisher != null) bookU.Publisher = dto.Publisher;
                if (dto.YearPublication != null) bookU.YearPublication = (int)dto.YearPublication;
                if (dto.Title != null) bookU.Title = dto.Title;
                if (dto.Quantity != null) bookU.Quantity = (int)dto.Quantity;

                var book = await _bookRepository.Update(bookU);

                return new BookUpdateDTO
                {
                    Author = book.Author,
                    Publisher = book.Publisher,
                    YearPublication = book.YearPublication,
                    Id = book.Id,
                    ISBN = book.ISBN,
                    Quantity = book.Quantity,
                    Title = book.Title
                };
            }
            catch(Exception ex)
            {
                Log.LogToFile("erro update", ex.Message);
                throw;
            }
        }
    
        public async Task<bool> Delete(Guid id)
        {
            try
            {
                var book = await _bookRepository.GetById(id) ?? throw new Exception("Livro não encontrado");

                book.IsDeleted = true;

                await _bookRepository.Delete(id);

                return true;
            }
            catch (Exception ex)
            {
                Log.LogToFile("delete service", ex.Message);
                throw;
            }

        }

    }
}
