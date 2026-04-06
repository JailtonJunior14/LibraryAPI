using Library.Data.DTOs;

namespace Library.Services.BookService
{
    public interface IBookService
    {
        public Task<IEnumerable<BookDTO>> GetAll();
        public Task<BookDTO> GetById(Guid id);
        public Task<IEnumerable<BookDTO?>> GetByTitle(string title);
        public Task<IEnumerable<BookDTO?>> GetByAuthor(string author);
        public Task<BookInsertDTO> Create(BookInsertDTO insertDTO);
        public Task<BookUpdateDTO> Update(BookUpdateDTO insertDTO);
        public Task<bool> Delete(Guid id);
    }
}
