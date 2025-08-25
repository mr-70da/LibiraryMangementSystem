using LibraryManagementSystem.Domain.Entities;
using LibraryManagementSystem.Domain.Interfaces.Repositories;
using LibraryManagementSystem.Infrastructure.Data;


namespace LibraryManagementSystem.Infrastructure.Repositories
{
    public class AuthorRepository :  GenericRepository<Author> , IAuthorRepository
    {
        public AuthorRepository(LibraryDbContext context) : base(context)
        {
        }
        public LibraryDbContext LibraryContext
        {
            get { return _context as LibraryDbContext; }
        }
    }
}
