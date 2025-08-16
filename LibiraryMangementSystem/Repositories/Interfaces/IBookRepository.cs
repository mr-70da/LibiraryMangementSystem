using LibraryManagementSystem.Dtos.Book;
using LibraryManagementSystem.Models;

namespace LibraryManagementSystem.Repositories
{
    public interface IBookRepository : IRepository<Book>
    {
        List<Book> GetByAuthor(int authorId);
        Book GetByIsbn(string isbn);
        void Update(int id ,Book book);
        void Create(Author auther, Book book);


    }
}
