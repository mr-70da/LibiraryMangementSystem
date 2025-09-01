using AutoMapper;
using LibraryManagementSystem.Application.DTOs;
using LibraryManagementSystem.Application.Queries.Books;
using LibraryManagementSystem.Domain.UnitOfWork;
using MediatR;
//done
namespace LibraryManagementSystem.Application.Handlers.Books
{
    public class GetMostBorrowedBooksHandler : IRequestHandler<GetMostBorrowedBooksQuery,
        GeneralResponse<List<MostBorrowedBooksDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetMostBorrowedBooksHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<GeneralResponse<List<MostBorrowedBooksDto>>>
            Handle(GetMostBorrowedBooksQuery request, CancellationToken cancellationToken)
        {
            try
            {
                GeneralResponse<List<MostBorrowedBooksDto>> response;
                var result = (from bh in await _unitOfWork.Borrowings.GetAllAsync()
                              join b in await _unitOfWork.Books.GetAllAsync()
                                  on bh.BookId equals b.Isbn
                              group bh by new { b.Isbn, b.Title } into g
                              orderby g.Count() descending
                              select new MostBorrowedBooksDto
                              {
                                  BookId = g.Key.Isbn,
                                  Title = g.Key.Title,
                                  BorrowCount = g.Count()
                              })
                              .Take(request.ListSize)
                              .ToList();
                response = new GeneralResponse<List<MostBorrowedBooksDto>>
                    (result, true, "Most borrowed books retrieved successfully", System.Net.HttpStatusCode.OK);
                return response;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving most borrowed books: " + ex.Message);
            }
        }
    }
}
