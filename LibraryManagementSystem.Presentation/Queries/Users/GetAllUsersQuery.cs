using LibraryManagementSystem.Application.DTOs;
using MediatR;
//done
namespace LibraryManagementSystem.Application.Queries.Users
{
    public class GetAllUsersQuery : IRequest<GeneralResponse<List<UserReadResponse>>>
    {
    }
}
