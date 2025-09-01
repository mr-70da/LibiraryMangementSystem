

using LibraryManagementSystem.Domain.Entities;
using LibraryManagementSystem.Domain.Interfaces.Repositories;
using LibraryManagementSystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;


namespace LibraryManagementSystem.Infrastructure.Repositories
{
    public class BookRepository : GenericRepository<Book>, IBookRepository
    {
        public BookRepository(LibraryDbContext context) : base(context)
        {
        }
        
        public async Task<List<Book>> GetAllByAuthorAsync(int authorId)
        {
            List<Book> returnedBooks = LibraryContext.Books

                                    .Where(b => b.AuthorId ==authorId)
                                    
                                    .ToList();
            Console.WriteLine(returnedBooks);

            return returnedBooks;
        }

        public async Task<List<Book>> GetFilteredBooksAsync
            (int? authorId, string? bookName, int? branchId)
        {
            //var query = await LibraryContext.Books.
            //    FromSqlInterpolated($"exec SearchBookWithFilters @AuthorID ={authorId},@BranchId ={branchId},@Title ={bookName};")
            //    .ToListAsync();
            var query = LibraryContext.Books.AsQueryable();
            if (authorId.HasValue)
            {
                query = query.Where(b => b.AuthorId == authorId.Value);
            }
            if (!string.IsNullOrEmpty(bookName))
            {
                query = query.Where(b => b.Title.Contains(bookName));
            }
            if (branchId.HasValue)
            {
                query = query.Where(b => b.BranchId == branchId.Value);
            }
            
            return query.ToList();
        }

        public LibraryDbContext LibraryContext
        {
            get { return _context as LibraryDbContext; }
        }
    }
}
