using LibraryManagementSystem.Application.DTOs;
using MediatR;

namespace LibraryManagementSystem.API.Queries
{
    public class GetMostBorrowedBooksQuery : IRequest<GeneralResponse<List<MostBorrowedBooksDto>>>
    {
       

    }
}
