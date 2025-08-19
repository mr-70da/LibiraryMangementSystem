using LibraryManagementSystem.Dtos.Book;
using LibraryManagementSystem.Models;

namespace LibraryManagementSystem.Services
{
    public interface IBookService
    {
        public void Update(int id, BookCreateDto UpdatedBook);
        public void Delete(int id);
        public void Create(BookCreateDto bookDto);
        public BooksByAuthorDto GetAllByAuthor(int authorId);
    }
}
