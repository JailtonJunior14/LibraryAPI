using Library.Data.DTOs;

namespace Library.Services.BookService
{
    public interface IBookService
    {
        public Task<IEnumerable<BookDTO>> GetAll();
        public Task<BookDTO> GetById(Guid id);
        public Task<BookInsertDTO> Create(BookInsertDTO insertDTO);
        public Task<BookInsertDTO> Update(BookInsertDTO insertDTO);
    }
}
