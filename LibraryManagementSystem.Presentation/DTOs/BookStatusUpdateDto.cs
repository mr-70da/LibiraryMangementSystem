using LibraryManagementSystem.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Application.DTOs
{
    public class BookStatusUpdateDto
    {
        public int BookIsbn { get; set; }
        public BookStatus BookStatus { get; set; }
    }
}
