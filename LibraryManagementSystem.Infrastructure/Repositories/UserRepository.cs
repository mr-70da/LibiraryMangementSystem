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
        public async Task<User> GetByEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }
        public async Task<String> GetRoleName(int roleId)
        {
            var name = await  _context.Users.Where(u => u.RoleId == roleId).
                Join(_context.Roles, u => u.RoleId, r => r.Id, (u, r) => r.Name).FirstOrDefaultAsync();
            return name;
        }


    }
}
