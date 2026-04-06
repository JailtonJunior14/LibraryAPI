using Library.Data.Entities;

namespace Library.Data.Repositorys.BookRepository
{
    public interface IBookRepository
    {
        public Task<IEnumerable<Book>> GetAll();
        public Task<Book?> GetById(Guid id);
        public Task<Book> Create(Book book);
        public Task<bool> ExistsISBN(long isbn);
        public Task<Book> GetByISBN(long isbn);
        public Task<IEnumerable<Book?>> GetByTitle(string title);
        public Task<IEnumerable<Book?>> GetByAuthor(string author);
        public Task<Book> Update(Book book);
        public Task<bool> Delete(Guid id);
    }
}
