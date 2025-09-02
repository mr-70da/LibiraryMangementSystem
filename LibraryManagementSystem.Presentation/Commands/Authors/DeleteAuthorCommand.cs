using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryManagementSystem.Application.DTOs;
using MediatR;

namespace LibraryManagementSystem.Application.Commands.Authors
{
    public class DeleteAuthorCommand : IRequest<GeneralResponse<AuthorReadResponse>>
    {
        public int Id { get; }
        public DeleteAuthorCommand(int id)
        {
            Id = id;
        }

    }
}
