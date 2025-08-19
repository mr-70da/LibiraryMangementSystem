using System.Security.Cryptography;
using LibraryManagementSystem.Models;
namespace LibraryManagementSystem.Repositories
{
    public interface IAuthorRepository : IGenricRepository<Author>
    {
        Author Get(int id);
        void Update(int id, Author updatedAuthor);
        void Add(Author author);
        void Remove(Author author);
    }
}
