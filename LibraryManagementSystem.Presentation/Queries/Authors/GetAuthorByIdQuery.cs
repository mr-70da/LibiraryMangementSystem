using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryManagementSystem.Application.DTOs;
using MediatR;
//done
namespace LibraryManagementSystem.Application.Queries.Authors
{
    public class GetAuthorByIdQuery : IRequest<GeneralResponse<AuthorReadResponse>>
    {
        public int Id { get; }
        public GetAuthorByIdQuery(int id)
        {
            Id = id;
        }
    }
}
