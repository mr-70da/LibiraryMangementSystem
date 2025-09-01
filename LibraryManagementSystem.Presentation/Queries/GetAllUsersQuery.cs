using LibraryManagementSystem.Application.DTOs;
using MediatR;
//done
namespace LibraryManagementSystem.API.Queries
{
    public class GetAllUsersQuery : IRequest<GeneralResponse<List<UserReadDto>>>
    {
    }
}
