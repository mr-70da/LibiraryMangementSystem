using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Application.DTOs
{
    public class BooksFilterResponse
    {
        public int TotalCount { get; set; }
        public List<BookReadDto> Books { get; set; }
    }
}
