
using LibraryManagementSystem.Application.DTOs;
using LibraryManagementSystem.Domain.Enums;

namespace LibraryManagementSystem.Domain.Interfaces.Services
{
    public interface IBookService
    {
        public Task UpdateAsync(BookUpdateRequestDto UpdatedBook);
        public Task DeleteAsync(int id);
        public Task CreateAsync(BookCreateDto bookDto);
        public Task<BooksFilterResponse> GetBooksAsync(BooksFilterRequest filter);
        public Task<List<BooksPerBranchDto>> GetBooksCountPerBranchAsync();
        public Task BorrowAsync(BorrowRequestDto borrowRequest);
        public Task ReturnAsync(int TransactionId);
        public Task<List<MostBorrowedBooksDto>> GetMostBorrowedAsync(int listSize);
        //just check status in service 
        public Task UpdateStatusAsync(BookStatusUpdateDto updateDto);
    }
}
