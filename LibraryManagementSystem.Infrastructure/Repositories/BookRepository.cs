

using LibraryManagementSystem.Domain.Entities;
using LibraryManagementSystem.Domain.Interfaces.Repositories;
using LibraryManagementSystem.Infrastructure.Data;


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

        public IQueryable<Book> GetFilteredBooks
            (int? authorId, string? bookName, int? branchId)
        {
            var query = LibraryContext.Books.AsQueryable();
            if(authorId.HasValue)
            {
                query = query.Where(b => b.AuthorId == authorId.Value);
            }
            if(!string.IsNullOrEmpty(bookName))
            {
                query = query.Where(b => b.Title.Contains(bookName));
            }
            if(branchId.HasValue)
            {
                query = query.Where(b => b.BranchId == branchId.Value);
            }
            return query;
        }

        public LibraryDbContext LibraryContext
        {
            get { return _context as LibraryDbContext; }
        }
    }
}
