using AutoMapper;
using MediatR;
using LibraryManagementSystem.Domain.Entities;
using LibraryManagementSystem.Domain.UnitOfWork;
using LibraryManagementSystem.Application.DTOs;
using LibraryManagementSystem.Application.Commands.Books;

namespace LibraryManagementSystem.Application.Handlers.Books
{
    public class UpdateBookHandler : IRequestHandler<UpdateBookCommand, GeneralResponse<BookReadDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateBookHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<GeneralResponse<BookReadDto>> Handle(UpdateBookCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (await _unitOfWork.Books.GetByIdAsync(request.BookDto.Isbn) == null)
                {
                    return new GeneralResponse<BookReadDto>
                        (null, false, "Book not found", System.Net.HttpStatusCode.NotFound);
                }

                var newBook = _mapper.Map<Book>(request.BookDto);
                _unitOfWork.Books.Update(newBook);
                await _unitOfWork.Complete();

                return new GeneralResponse<BookReadDto>
                    (_mapper.Map<BookReadDto>(newBook), true,
                    "Book updated successfully", System.Net.HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while updating the book: " + ex.Message);
            }
        }
    }
}