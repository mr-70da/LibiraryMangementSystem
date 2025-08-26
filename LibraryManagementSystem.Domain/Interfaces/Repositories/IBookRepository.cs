
using LibraryManagementSystem.Domain.Entities;

namespace LibraryManagementSystem.Domain.Interfaces.Repositories
{
    public interface IBookRepository : IGenericRepository<Book>
    {
        Task<List<Book>> GetAllByAuthorAsync(int authorId);
        Task<IQueryable<Book>> GetFilteredBooksAsync
            (int? authorId, string? bookName, int? branchId);



    }
}
