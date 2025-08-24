
using LibraryManagementSystem.Domain.Interfaces.Repositories;
using LibraryManagementSystem.Domain.Entities;

namespace LibraryManagementSystem.Domain.UnitOfWork
{
    public interface IUnitOfWork :IDisposable
    {
        IGenericRepository<Author> Authors { get; }
        IBookRepository Books { get; }
        IUserRepository Users { get; }
        int Complete();
        

    }
}
