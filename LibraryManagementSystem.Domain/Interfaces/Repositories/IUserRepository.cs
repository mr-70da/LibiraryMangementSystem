using LibraryManagementSystem.Domain.Entities;


namespace LibraryManagementSystem.Domain.Interfaces.Repositories
{
    public interface IUserRepository : IGenericRepository<User>
    {
        public Task<IEnumerable<BorrowingHistory>> GetBorrowingHistoryAsync(int userId);
    }
}
