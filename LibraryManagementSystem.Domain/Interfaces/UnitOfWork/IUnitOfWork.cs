
using LibraryManagementSystem.Domain.Interfaces.Repositories;
using LibraryManagementSystem.Domain.Entities;

namespace LibraryManagementSystem.Domain.UnitOfWork
{
    public interface IUnitOfWork :IDisposable
    {
        IAuthorRepository Authors { get; }
        IBookRepository Books { get; }
        IUserRepository Users { get; }
        IBorrowingRepository Borrowings { get; }
        IBranchRepository Branches { get; }
       
        int Complete();
        

    }
}
