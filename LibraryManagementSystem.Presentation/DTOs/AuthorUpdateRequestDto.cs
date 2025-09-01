using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Application.DTOs
{
    public class AuthorUpdateRequestDto : AuthorCreateDto
    {
        public int Id { get; set; }
    }
}
