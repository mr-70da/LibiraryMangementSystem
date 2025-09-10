using AutoMapper;
using LibraryManagementSystem.Application.DTOs;
using LibraryManagementSystem.Application.Queries.Books;
using LibraryManagementSystem.Domain.UnitOfWork;
using MediatR;
using Microsoft.Extensions.Logging;
//done
namespace LibraryManagementSystem.Application.Handlers.Books
{
    public class GetMostBorrowedBooksHandler : IRequestHandler<GetMostBorrowedBooksQuery,
        GeneralResponse<List<MostBorrowedBooksResponse>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<GetMostBorrowedBooksHandler> _logger;

        public GetMostBorrowedBooksHandler(IUnitOfWork unitOfWork, IMapper mapper ,
            ILogger<GetMostBorrowedBooksHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<GeneralResponse<List<MostBorrowedBooksResponse>>>
            Handle(GetMostBorrowedBooksQuery request, CancellationToken cancellationToken)
        {
            try
            {
                GeneralResponse<List<MostBorrowedBooksResponse>> response;
                var result = (from bh in await _unitOfWork.Borrowings.GetAllAsync()
                              join b in await _unitOfWork.Books.GetAllAsync()
                                  on bh.BookId equals b.Isbn
                              group bh by new { b.Isbn, b.Title } into g
                              orderby g.Count() descending
                              select new MostBorrowedBooksResponse
                              {
                                  BookId = g.Key.Isbn,
                                  Title = g.Key.Title,
                                  BorrowCount = g.Count()
                              })
                              .Take(request.ListSize)
                              .ToList();
                _logger.LogInformation("Most borrowed books retrieved successfully.");
                response = new GeneralResponse<List<MostBorrowedBooksResponse>>
                    (result, true, "Most borrowed books retrieved successfully", System.Net.HttpStatusCode.OK);
                return response;
            }
            catch (Exception ex)
            {
                
                throw new Exception( ex.Message);
            }
        }
    }
}
