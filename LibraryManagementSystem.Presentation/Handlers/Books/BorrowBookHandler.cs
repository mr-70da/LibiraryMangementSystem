using AutoMapper;
using MediatR;
using LibraryManagementSystem.Domain.Entities;
using LibraryManagementSystem.Domain.UnitOfWork;
using LibraryManagementSystem.Domain.Enums;
using LibraryManagementSystem.Application.DTOs;
using LibraryManagementSystem.Application.Commands.Books;

namespace LibraryManagementSystem.Application.Handlers.Books
{
    public class BorrowBookHandler : IRequestHandler<BorrowBookCommand, GeneralResponse<BookReadDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public BorrowBookHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<GeneralResponse<BookReadDto>> Handle(BorrowBookCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (request.RequestDto.UserId == null || request.RequestDto.BookIsbn == null)
                {
                    return new GeneralResponse<BookReadDto>
                        (null, false, "User id and book ISBN should be specified",
                        System.Net.HttpStatusCode.BadRequest);
                }

                var user = await _unitOfWork.Users.GetByIdAsync(request.RequestDto.UserId);
                if (user == null)
                {
                    return new GeneralResponse<BookReadDto>
                        (null, false, "User not found", System.Net.HttpStatusCode.NotFound);
                }

                var book = await _unitOfWork.Books.GetByIdAsync(request.RequestDto.BookIsbn);
                if (book == null)
                {
                    return new GeneralResponse<BookReadDto>
                        (null, false, "Book not found", System.Net.HttpStatusCode.NotFound);
                }

                if (!IsBookAvailable(book))
                {
                    return new GeneralResponse<BookReadDto>
                        (null, false, "Book is not available", System.Net.HttpStatusCode.BadRequest);
                }

                var borrowingHistory = new BorrowingHistory
                {
                    UserId = request.RequestDto.UserId,
                    BookId = request.RequestDto.BookIsbn,
                    BorrowDate = DateOnly.FromDateTime(DateTime.Now)
                };

                book.Status = BookStatus.Borrowed;
                _unitOfWork.Books.Update(book);
                await _unitOfWork.Borrowings.AddAsync(borrowingHistory);
                await _unitOfWork.Complete();

                return new GeneralResponse<BookReadDto>
                    (_mapper.Map<BookReadDto>(book), true,
                    "Book borrowed successfully", System.Net.HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while borrowing the book: " + ex.Message);
            }
        }

        private bool IsBookAvailable(Book book)
        {
            return book.Status == BookStatus.Available;
        }
    }
}
