using System.Net;
using AutoMapper;
using LibraryManagementSystem.Application.DTOs;
using LibraryManagementSystem.Application.Queries.Books;
using LibraryManagementSystem.Domain.UnitOfWork;
using MediatR;
using Microsoft.Extensions.Logging;
//done
namespace LibraryManagementSystem.Application.Handlers.Books
{
    public class GetBooksHandler : IRequestHandler<GetAllBooksQuery, GeneralResponse<BooksFilterResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private
            readonly ILogger<GetBooksHandler> _logger;

        public GetBooksHandler(IUnitOfWork unitOfWork, IMapper mapper ,ILogger<GetBooksHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<GeneralResponse<BooksFilterResponse>> 
            Handle(GetAllBooksQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var query = await _unitOfWork.Books.GetFilteredBooksAsync(
                request.AuthorId, request.BookName, request.BranchId);

                int totalBooks = query.Count();

                var paginatedBooks = query
                    .Skip(request.Skip)
                    .Take(request.Take)
                    .ToList();

                var bookDto = _mapper.Map<List<BookReadResponse>>(paginatedBooks);
                _logger.LogInformation("Retrieved {Count} books with filters - AuthorId: {AuthorId}, BookName: {BookName}, BranchId: {BranchId}",
                    bookDto.Count, request.AuthorId, request.BookName, request.BranchId);
                return new GeneralResponse<BooksFilterResponse>(
                    new BooksFilterResponse
                    {
                        TotalCount = totalBooks,
                        Books = bookDto
                    },
                    true, "Books retrieved successfully", HttpStatusCode.OK);
            }catch (Exception ex)
            
                {
                throw new Exception(ex.Message);
                }
        }
    }
}
