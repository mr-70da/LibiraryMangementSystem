using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Application.DTOs
{
    public class UserBorrowingHistoryDto
    {
        public String BookTitle { get; set; }
        public DateOnly BorrowDate { get; set; }

        public DateOnly? ReturnDate { get; set; }
    }
}
