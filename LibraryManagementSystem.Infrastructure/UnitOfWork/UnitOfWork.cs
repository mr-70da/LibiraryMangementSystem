using LibraryManagementSystem.Domain.Entities;
using LibraryManagementSystem.Domain.Interfaces.Repositories;
using LibraryManagementSystem.Domain.UnitOfWork;
using LibraryManagementSystem.Infrastructure.Data;
using LibraryManagementSystem.Infrastructure.Repositories;
using LibraryManagementSystem.Infrastructure.Repositories.Implementation;


namespace LibraryManagementSystem.Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly LibraryDbContext _context;
        public UnitOfWork(LibraryDbContext context , IBookRepository bookRepository,
            IUserRepository userRepository,
            IGenericRepository<Author> authorRepository)
        {
            _context = context;
            Books = bookRepository;
            Authors = authorRepository;
            Users = userRepository;
        }

        public IGenericRepository<Author> Authors { get; private set; }
        public IBookRepository  Books { get; private set; }
        public IUserRepository Users { get; private set; }


        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose() { 
        
            _context.Dispose();
        
        }


        
    }
}
