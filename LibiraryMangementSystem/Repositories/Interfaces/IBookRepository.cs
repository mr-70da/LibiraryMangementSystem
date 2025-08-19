using LibraryManagementSystem.Dtos.Book;
using LibraryManagementSystem.Models;

namespace LibraryManagementSystem.Repositories
{
    public interface IBookRepository : IGenericRepository<Book>
    {
        List<Book> GetAllByAuthor(int authorId);
        


    }
}
