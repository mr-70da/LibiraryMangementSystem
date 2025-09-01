using LibraryManagementSystem.Application.DTOs;
using MediatR;

namespace LibraryManagementSystem.API.Queries
{
    public class GetAllBooksQuery :  IRequest<GeneralResponse<List<BooksFilterResponse>>>
    {


    }
}
