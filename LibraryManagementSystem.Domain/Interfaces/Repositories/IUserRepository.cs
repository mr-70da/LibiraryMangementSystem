using LibraryManagementSystem.Domain.Entities;


namespace LibraryManagementSystem.Domain.Interfaces.Repositories
{
    public interface IUserRepository : IGenericRepository<User>
    {
        public IEnumerable<BorrowingHistory> GetBorrowingHistory(int userId);
    }
}
