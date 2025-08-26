using LibraryManagementSystem.Domain.Entities;
using LibraryManagementSystem.Domain.Interfaces.Repositories;
using LibraryManagementSystem.Domain.UnitOfWork;
using LibraryManagementSystem.Infrastructure.Data;



namespace LibraryManagementSystem.Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly LibraryDbContext _context;
        public UnitOfWork(LibraryDbContext context , IBookRepository bookRepository,
            IUserRepository userRepository,
            IAuthorRepository authorRepository,
            IBorrowingRepository borrowingRepository,
            IBranchRepository branchRepository)
        {
            _context = context;
            Books = bookRepository;
            Authors = authorRepository;
            Borrowings = borrowingRepository;
            Branches = branchRepository;
            Users = userRepository;
        }

        public IAuthorRepository Authors { get; private set; }
        public IBorrowingRepository Borrowings { get; }
        public IBranchRepository Branches { get; }
        public IBookRepository  Books { get; private set; }
        public IUserRepository Users { get; private set; }


        public async Task<int> Complete()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose() { 
        
            _context.Dispose();
        
        }


        
    }
}
