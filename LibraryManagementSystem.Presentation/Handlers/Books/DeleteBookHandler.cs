using AutoMapper;
using MediatR;
using LibraryManagementSystem.Domain.UnitOfWork;
using LibraryManagementSystem.Application.DTOs;
using LibraryManagementSystem.Application.Commands.Books;

namespace LibraryManagementSystem.Application.Handlers.Books
{
    public class DeleteBookHandler : IRequestHandler<DeleteBookCommand, GeneralResponse<BookReadDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteBookHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<GeneralResponse<BookReadDto>> Handle(DeleteBookCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var book = await _unitOfWork.Books.GetByIdAsync(request.Id);
                if (book == null)
                {
                    return new GeneralResponse<BookReadDto>
                        (null, false, "Book not found", System.Net.HttpStatusCode.NotFound);
                }

                _unitOfWork.Books.Remove(book);
                await _unitOfWork.Complete();

                return new GeneralResponse<BookReadDto>
                    (_mapper.Map<BookReadDto>(book), true,
                    "Book deleted successfully", System.Net.HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while deleting the book: " + ex.Message);
            }
        }
    }
}
