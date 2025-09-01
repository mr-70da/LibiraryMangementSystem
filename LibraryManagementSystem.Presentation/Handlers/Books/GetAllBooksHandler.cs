using System.Net;
using AutoMapper;
using LibraryManagementSystem.Application.DTOs;
using LibraryManagementSystem.Application.Queries.Books;
using LibraryManagementSystem.Domain.UnitOfWork;
using MediatR;
//done
namespace LibraryManagementSystem.Application.Handlers.Books
{
    public class GetBooksHandler : IRequestHandler<GetAllBooksQuery, GeneralResponse<BooksFilterResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetBooksHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<GeneralResponse<BooksFilterResponse>> Handle(GetAllBooksQuery request, CancellationToken cancellationToken)
        {
            var query = await _unitOfWork.Books.GetFilteredBooksAsync(
                request.Filter.AuthorId, request.Filter.BookName, request.Filter.BranchId);

            int totalBooks = query.Count();

            var paginatedBooks = query
                .Skip(request.Filter.Skip)
                .Take(request.Filter.Take)
                .ToList();

            var bookDto = _mapper.Map<List<BookReadDto>>(paginatedBooks);

            return new GeneralResponse<BooksFilterResponse>(
                new BooksFilterResponse
                {
                    TotalCount = totalBooks,
                    Books = bookDto
                },
                true, "Books retrieved successfully", HttpStatusCode.OK);
        }
    }
}
