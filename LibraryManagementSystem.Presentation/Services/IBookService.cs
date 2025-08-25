
using LibraryManagementSystem.Application.DTOs;
using LibraryManagementSystem.Domain.Enums;

namespace LibraryManagementSystem.Domain.Interfaces.Services
{
    public interface IBookService
    {
        public void Update(int id, BookCreateDto UpdatedBook);
        public void Delete(int id);
        public void Create(BookCreateDto bookDto);
        public BooksByAuthorDto GetBooks(BookFilterDto filter);
        public List<BooksPerBranchDto> GetBooksCountPerBranch();
        public void Borrow(int UserId, int BookIsbn);
        public void Return(int TransactionId);
        public List<MostBorrowedBooksDto> GetMostBorrowed(int listSize);
        //just check status in service 
        public void UpdateStatus(int bookIsbn , BookStatus bookStatus);
    }
}
