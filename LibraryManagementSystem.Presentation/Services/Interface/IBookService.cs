using LibraryManagementSystem.Application.DTOs;
using LibraryManagementSystem.Domain.Enums;

namespace LibraryManagementSystem.Application.Services.Interface
{
    public interface IBookService
    {
        //Read
        public Task<GeneralResponse<List<MostBorrowedBooksDto>>> GetMostBorrowedAsync(int listSize);
        public Task<GeneralResponse<BooksFilterResponse>> GetBooksAsync(BooksFilterRequest filter);
        public Task<GeneralResponse<List<BooksPerBranchDto>>> GetBooksCountPerBranchAsync();
        //others
        public Task<GeneralResponse<BookReadDto>> UpdateAsync(BookUpdateRequestDto UpdatedBook);
        public Task<GeneralResponse<BookReadDto>> DeleteAsync(int id);
        public Task<GeneralResponse<BookReadDto>> CreateAsync(BookCreateDto bookDto);
        public Task<GeneralResponse<BookReadDto>> BorrowAsync(BorrowRequestDto borrowRequest);
        public Task<GeneralResponse<BookReadDto>> ReturnAsync(int TransactionId);
        //just check status in service 
        public Task<GeneralResponse<BookReadDto>> UpdateStatusAsync(BookStatusUpdateDto updateDto);
    }
}
