using AutoMapper;
using MediatR;
using LibraryManagementSystem.Domain.Entities;
using LibraryManagementSystem.Domain.UnitOfWork;
using LibraryManagementSystem.Application.DTOs;
using LibraryManagementSystem.Application.Commands.Books;
using Microsoft.Extensions.Logging;

namespace LibraryManagementSystem.Application.Handlers.Books
{
    public class UpdateBookHandler : IRequestHandler<UpdateBookCommand, GeneralResponse<BookReadResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateBookHandler> _logger;
        public UpdateBookHandler(IUnitOfWork unitOfWork, IMapper mapper , ILogger<UpdateBookHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<GeneralResponse<BookReadResponse>> Handle(UpdateBookCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (await _unitOfWork.Books.GetByIdAsync(request.Isbn) == null)
                {
                    _logger.LogWarning("Attempted to update non-existent book with ISBN: {BookIsbn}", request.Isbn);
                    return new GeneralResponse<BookReadResponse>
                        (null, false, "Book not found", System.Net.HttpStatusCode.NotFound);
                }

                var newBook = _mapper.Map<Book>(request);
                _unitOfWork.Books.Update(newBook);
                await _unitOfWork.Complete();
                _logger.LogInformation("Book updated successfully with ISBN: {BookIsbn}", request.Isbn);
                return new GeneralResponse<BookReadResponse>
                    (_mapper.Map<BookReadResponse>(newBook), true,
                    "Book updated successfully", System.Net.HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating the book with ISBN: {BookIsbn}", request.Isbn);
                throw new Exception("An error occurred while updating the book: " + ex.Message);
            }
        }
    }
}