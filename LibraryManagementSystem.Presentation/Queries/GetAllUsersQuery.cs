using LibraryManagementSystem.Application.DTOs;
using MediatR;

namespace LibraryManagementSystem.API.Queries
{
    public class GetAllUsersQuery : IRequest<GeneralResponse<List<UserReadDto>>>
    {
    }
}
