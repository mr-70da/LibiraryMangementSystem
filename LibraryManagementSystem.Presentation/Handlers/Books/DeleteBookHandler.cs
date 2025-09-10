using AutoMapper;
using MediatR;
using LibraryManagementSystem.Domain.UnitOfWork;
using LibraryManagementSystem.Application.DTOs;
using LibraryManagementSystem.Application.Commands.Books;
using Microsoft.Extensions.Logging;

namespace LibraryManagementSystem.Application.Handlers.Books
{
    public class DeleteBookHandler : IRequestHandler<DeleteBookCommand, GeneralResponse<BookReadResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<DeleteBookHandler> _logger;

        public DeleteBookHandler(IUnitOfWork unitOfWork, IMapper mapper , ILogger<DeleteBookHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<GeneralResponse<BookReadResponse>> 
            Handle(DeleteBookCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var book = await _unitOfWork.Books.GetByIdAsync(request.Id);
                if (book == null)
                {
                    _logger.LogWarning("Attempted to delete non-existent book with ISBN: {BookIsbn}", request.Id);
                    return new GeneralResponse<BookReadResponse>
                        (null, false, "Book not found", System.Net.HttpStatusCode.NotFound);
                }

                _unitOfWork.Books.Remove(book);
                await _unitOfWork.Complete();
                _logger.LogInformation("Book deleted successfully with ISBN: {BookIsbn}", request.Id);

                return new GeneralResponse<BookReadResponse>
                    (_mapper.Map<BookReadResponse>(book), true,
                    "Book deleted successfully", System.Net.HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                
                throw new Exception(ex.Message);
            }
        }
    }
}
