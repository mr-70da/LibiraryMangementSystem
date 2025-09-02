using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryManagementSystem.Application.DTOs;
using MediatR;

namespace LibraryManagementSystem.Application.Commands.Authors
{
    public class UpdateAuthorCommand : IRequest<GeneralResponse<AuthorReadResponse>>
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string? LastName { get; set; }
        public UpdateAuthorCommand(int Id, String FirstName , String? LastName)
        {
             this.FirstName = FirstName;
             this.LastName = LastName;
             this.Id = Id;
        }
       
    }
}
