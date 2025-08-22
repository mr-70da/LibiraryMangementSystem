using LibraryManagementSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Domain.Interfaces.Repositories
{
    public interface IUserRepository : IGenericRepository<User>
    {
        public IEnumerable<BorrowingHistory> GetBorrowingHistory(int userId);
    }
}
