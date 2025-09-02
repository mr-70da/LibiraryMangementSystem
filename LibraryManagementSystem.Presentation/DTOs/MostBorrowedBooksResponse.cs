using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Application.DTOs
{
    public class MostBorrowedBooksResponse
    {
        public int BookId { get; set; }
        public string Title { get; set; }
        public int BorrowCount { get; set; }
    }
}
