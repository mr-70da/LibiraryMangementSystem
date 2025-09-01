using LibraryManagementSystem.Application.DTOs;
using MediatR;
//done
namespace LibraryManagementSystem.Application.Queries.Books
{
    public class GetBooksCountPerBranchQuery : IRequest<GeneralResponse<List<BooksPerBranchDto>>>
    {
        
    }
}
