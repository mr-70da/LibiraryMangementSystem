using LibraryManagementSystem.Repositories;

namespace LibraryManagementSystem.UnitOfWork
{
    public interface IUnitOfWork :IDisposable
    {
        IAuthorRepository Authors { get; }
        IBookRepository Books { get; }
        int Complete();
        

    }
}
