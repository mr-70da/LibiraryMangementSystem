using LibraryManagementSystem.Application.Service;
using LibraryManagementSystem.Domain.Entities;
using LibraryManagementSystem.Domain.Interfaces.Repositories;
using LibraryManagementSystem.Infrastructure.Data;

namespace LibraryManagementSystem.Infrastructure.Repositories
{
    public class BorrowingRepository : GenericRepository<BorrowingHistory>, IBorrowingRepository
    {
        
        public BorrowingRepository(LibraryDbContext context, ICacheService cache) : base(context, cache)
        {
        }
        public LibraryDbContext LibraryContext
        {
            get { return _context as LibraryDbContext; }
        }

    }
}
