
using LibraryManagementSystem.Domain.Entities;

namespace LibraryManagementSystem.Domain.Interfaces.Repositories
{
    public interface IBookRepository : IGenericRepository<Book>
    {
        List<Book> GetAllByAuthor(int authorId);
        IQueryable<Book> GetFilteredBooks
            (int? authorId, string? bookName, int? branchId);



    }
}
