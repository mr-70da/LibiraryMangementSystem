using LibraryManagementSystem.Models;
using LibraryManagementSystem.Repositories;
using LibraryManagementSystem.Repositories.Implementation;

namespace LibraryManagementSystem.UnitOfWork
{
    public interface IUnitOfWork :IDisposable
    {
        IGenericRepository<Author> Authors { get; }
        IBookRepository Books { get; }
        int Complete();
        

    }
}
