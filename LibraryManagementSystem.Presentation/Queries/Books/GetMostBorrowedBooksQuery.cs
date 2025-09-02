using LibraryManagementSystem.Application.DTOs;
using MediatR;
//done
namespace LibraryManagementSystem.Application.Queries.Books
{
    public class GetMostBorrowedBooksQuery : IRequest<GeneralResponse<List<MostBorrowedBooksResponse>>>
    {
       public int ListSize { get; }
         public GetMostBorrowedBooksQuery(int listSize)
         {
              ListSize = listSize;
        }

    }
}
