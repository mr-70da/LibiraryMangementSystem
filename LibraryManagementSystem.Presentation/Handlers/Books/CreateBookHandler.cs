using AutoMapper;
using LibraryManagementSystem.Application.Commands.Books;
using LibraryManagementSystem.Application.DTOs;
using LibraryManagementSystem.Domain.Entities;
using LibraryManagementSystem.Domain.UnitOfWork;
using MediatR;
using System.Net;
//done
namespace LibraryManagementSystem.Application.Handlers.Books
{
    public class CreateBookHandler : IRequestHandler<CreateBookCommand, GeneralResponse<BookReadDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateBookHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<GeneralResponse<BookReadDto>> Handle(CreateBookCommand request, CancellationToken cancellationToken)
        {
            var newBook = _mapper.Map<Book>(request.BookDto);
            var author = await _unitOfWork.Authors.GetByIdAsync(request.BookDto.AuthorId);

            if (author == null)
            {
                return new GeneralResponse<BookReadDto>(
                    null, false, "Author not found", HttpStatusCode.BadRequest);
            }

            await _unitOfWork.Books.AddAsync(newBook);
            await _unitOfWork.Complete();

            return new GeneralResponse<BookReadDto>(
                _mapper.Map<BookReadDto>(newBook), true, "Book created successfully", HttpStatusCode.Created);
        }
    }
}
