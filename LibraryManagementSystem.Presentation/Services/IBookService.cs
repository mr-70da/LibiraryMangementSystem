
using LibraryManagementSystem.Application.DTOs;
using LibraryManagementSystem.Domain.Enums;

namespace LibraryManagementSystem.Domain.Interfaces.Services
{
    public interface IBookService
    {
        public Task UpdateAsync(int id, BookCreateDto UpdatedBook);
        public Task DeleteAsync(int id);
        public Task CreateAsync(BookCreateDto bookDto);
        public Task<BooksByAuthorDto> GetBooksAsync(BookFilterDto filter);
        public Task<List<BooksPerBranchDto>> GetBooksCountPerBranchAsync();
        public Task BorrowAsync(int UserId, int BookIsbn);
        public Task ReturnAsync(int TransactionId);
        public Task<List<MostBorrowedBooksDto>> GetMostBorrowedAsync(int listSize);
        //just check status in service 
        public Task UpdateStatusAsync(int bookIsbn , BookStatus bookStatus);
    }
}
