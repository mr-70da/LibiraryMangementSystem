using AutoMapper;
using LibraryManagementSystem.Application.Commands.Books;
using LibraryManagementSystem.Application.DTOs;
using LibraryManagementSystem.Domain.Entities;
using LibraryManagementSystem.Domain.UnitOfWork;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Net;
//done
namespace LibraryManagementSystem.Application.Handlers.Books
{
    public class CreateBookHandler : IRequestHandler<CreateBookCommand, GeneralResponse<BookReadResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateBookHandler> _logger;

        public CreateBookHandler(IUnitOfWork unitOfWork, IMapper mapper,ILogger<CreateBookHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<GeneralResponse<BookReadResponse>> Handle(CreateBookCommand request, CancellationToken cancellationToken)
        {
            var newBook = _mapper.Map<Book>(request);
            var author = await _unitOfWork.Authors.GetByIdAsync(request.AuthorId);

            if (author == null)
            {
                _logger.LogWarning("Attempted to create a book with non-existent author ID: {AuthorId}", request.AuthorId);
                return new GeneralResponse<BookReadResponse>(
                    null, false, "Author not found", HttpStatusCode.BadRequest);
            }

            await _unitOfWork.Books.AddAsync(newBook);
            await _unitOfWork.Complete();
            _logger.LogInformation("Book created successfully with ISBN: {BookIsbn}", newBook.Isbn);
            return new GeneralResponse<BookReadResponse>(
                _mapper.Map<BookReadResponse>(newBook), true, "Book created successfully", HttpStatusCode.Created);
        }
    }
}
