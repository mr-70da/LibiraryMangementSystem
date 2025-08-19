using LibraryManagementSystem.Data;
using LibraryManagementSystem.Dtos.Book;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.Repositories.Implementation;
using Microsoft.EntityFrameworkCore.Query;

namespace LibraryManagementSystem.Repositories
{
    public class BookRepository : GenericRepository<Book>, IBookRepository
    {
        public BookRepository(LibraryContext context) : base(context)
        {
        }
        
        public List<Book> GetAllByAuthor(int authorId)
        {
            List<Book> returnedBooks = LibraryContext.Books
                                    .Where(b => b.Authors.Any(a => a.Id == authorId))
                                    .ToList();

            return returnedBooks;
        }

        public LibraryContext LibraryContext
        {
            get { return _context as LibraryContext; }
        }
    }
}
