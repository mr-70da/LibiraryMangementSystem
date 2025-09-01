using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryManagementSystem.Application.DTOs;
using MediatR;

namespace LibraryManagementSystem.Application.Commands.Authors
{
    public class UpdateAuthorCommand : IRequest<GeneralResponse<AuthorReadDto>>
    {
        public AuthorUpdateRequestDto UpdatedAuthorDto { get; }
        public UpdateAuthorCommand(AuthorUpdateRequestDto updatedAuthorDto)
        {
            UpdatedAuthorDto = updatedAuthorDto;
        }
       
    }
}
