using LibraryManagementSystem.Application.DTOs;
using MediatR;

namespace LibraryManagementSystem.API.Queries
{
    public class GetBooksCountPerBranchQuery : IRequest<GeneralResponse<List<BooksPerBranchDto>>>
    {
        
    }
}
