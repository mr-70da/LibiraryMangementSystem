using LibraryManagementSystem.Domain.Entities;
using LibraryManagementSystem.Domain.Interfaces.Repositories;
using LibraryManagementSystem.Infrastructure.Data;

using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.Infrastructure.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(LibraryDbContext context) : base(context)
        {
        }

        public async Task <IEnumerable<BorrowingHistory>> GetBorrowingHistoryAsync(int userId)
        {
            return _context.BorrowingHistories
                .Where(bh => bh.UserId.HasValue && bh.UserId.Value == userId)
                .Include(bh => bh.Book)
                .ToList();
        }

        
    }
}
