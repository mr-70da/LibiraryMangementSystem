using LibraryManagementSystem.Application.DTOs;
using LibraryManagementSystem.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Application.Commands.Authentications
{
    public class RegisterCommand : IRequest<GeneralResponse<LoginResponse>>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int RoleId { get; set; }
        public RegisterCommand(String FirstName, string LastName , string Email ,string Password ,int RoleId )
        {
            this.FirstName = FirstName;
            this.LastName= LastName;
            this.Email = Email;
            this.Password = Password;
            this.RoleId = RoleId;
            
        }
    }

  
}
