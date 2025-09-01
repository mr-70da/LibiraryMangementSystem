using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryManagementSystem.Application.DTOs;
using MediatR;

namespace LibraryManagementSystem.Application.Commands.Users
{
    public class CreateUserCommand : IRequest<GeneralResponse<UserReadDto>>
    {
        
        public UserCreateDto CreateUserDto { get; }
        public CreateUserCommand(UserCreateDto createUserDto)
        {
            CreateUserDto = createUserDto;
        }
    }
}
