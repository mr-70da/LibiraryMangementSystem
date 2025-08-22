

using LibraryManagementSystem.Domain.Entities;
using LibraryManagementSystem.Domain.Interfaces.Repositories;
using LibraryManagementSystem.Infrastructure.Data;



using LibraryManagementSystem.Infrastructure.Repositories.Implementation;

namespace LibraryManagementSystem.Infrastructure.Repositories
{
    public class BookRepository : GenericRepository<Book>, IBookRepository
    {
        public BookRepository(LibraryDbContext context) : base(context)
        {
        }
        
        public List<Book> GetAllByAuthor(int authorId)
        {
            List<Book> returnedBooks = LibraryContext.Books
                                    .Where(b => b.AuthorId ==authorId)
                                    .ToList();

            return returnedBooks;
        }

        public LibraryDbContext LibraryContext
        {
            get { return _context as LibraryDbContext; }
        }
    }
}
