using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Application.DTOs
{
    public class BookReadResponse
    {
        public int Isbn { get; set; }
        public int AuthorId { get; set; }
        public string Title { get; set; }
        public int Edition { get; set; }
        public short? CopyRightYear { get; set; }
        public decimal Price { get; set; }
    }
}
