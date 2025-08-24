using LibraryManagementSystem.Domain.Entities;
using LibraryManagementSystem.Domain.Interfaces.Repositories;
using LibraryManagementSystem.Infrastructure.Data;
using LibraryManagementSystem.Infrastructure.Repositories.Implementation;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.Infrastructure.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(LibraryDbContext context) : base(context)
        {
        }

        public IEnumerable<BorrowingHistory> GetBorrowingHistory(int userId)
        {
            return _context.BorrowingHistories
                .Where(bh => bh.UserId.HasValue && bh.UserId.Value == userId)
                .Include(bh => bh.Book)
                .ToList();
        }
    }
}
