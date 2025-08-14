using LibraryManagementSystem.Data;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.Repositories;


namespace LibraryManagementSystem.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly LibraryContext _context;
        public UnitOfWork(LibraryContext context)
        {
            _context = context;
            Books = new BookRepository(_context);
            Authors = new AuthorRepository(_context);
        }

        public IAuthorRepository Authors { get; private set; }
        public IBookRepository Books { get; private set; }

     
        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose() { 
        
            _context.Dispose();
        
        }


        
    }
}
