using LibraryManagementSystem.Data;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.Repositories;
using LibraryManagementSystem.Repositories.Implementation;


namespace LibraryManagementSystem.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly LibraryContext _context;
        public UnitOfWork(LibraryContext context , IBookRepository bookRepository,
        IGenericRepository<Author> authorRepository)
        {
            _context = context;
            Books = bookRepository;
            Authors = authorRepository;
        }

        public IGenericRepository<Author> Authors { get; private set; }
        public IBookRepository  Books { get; private set; }

        

        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose() { 
        
            _context.Dispose();
        
        }


        
    }
}
