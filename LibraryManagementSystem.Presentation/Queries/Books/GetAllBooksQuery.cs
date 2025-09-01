using LibraryManagementSystem.Application.DTOs;
using MediatR;
//done
namespace LibraryManagementSystem.Application.Queries.Books
{
    public class GetAllBooksQuery : IRequest<GeneralResponse<BooksFilterResponse>>
    {
        public BooksFilterRequest Filter { get; }
        public GetAllBooksQuery(BooksFilterRequest filter)
        {
            Filter = filter;
        }
    }
}
