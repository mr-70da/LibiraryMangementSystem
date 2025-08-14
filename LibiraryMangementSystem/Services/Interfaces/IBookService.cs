using LibraryManagementSystem.Dtos.Book;
using LibraryManagementSystem.Models;

namespace LibraryManagementSystem.Services
{
    public interface IBookService
    {
        public void Update(int id, BookCreateDto UpdatedBook);
        public void Delete(int id);
        public void Create(int autherId, BookCreateDto bookDto);
        public IEnumerable<BookReadDto> GetAllByAuthor(int authorId);
    }
}
