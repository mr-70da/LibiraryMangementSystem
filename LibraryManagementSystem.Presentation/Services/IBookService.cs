
using LibraryManagementSystem.Application.DTOs;

namespace LibraryManagementSystem.Domain.Interfaces.Services
{
    public interface IBookService
    {
        public void Update(int id, BookCreateDto UpdatedBook);
        public void Delete(int id);
        public void Create(BookCreateDto bookDto);
        public BooksByAuthorDto GetAllByAuthor(int authorId);
        public List<BookReadDto> GetBooksCountPerBranch();
        public void Borrow(int UserId, int BookIsbn, int BranchId);
        public void Return(int TransactionId);
        public List<BookReadDto> GetMostBorrowed(int listSize);
        //just check status in service 
        public void UpdateStatus(int bookIsbn);
    }
}
