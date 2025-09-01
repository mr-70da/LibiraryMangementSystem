using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Application.Queries
{
    internal class GetAuthorByIdQuery
    {
        public int Id { get; }
        public GetAuthorByIdQuery(int id)
        {
            Id = id;
        }
    }
}
