namespace LibraryManagementSystem.Application.DTOs
{
    public class BooksFilterRequest
    {
       
        public int? AuthorId { get; set; } 
        public string? BookName { get; set; }
        public int? BranchId { get; set; }

    
        public int Skip { get; set; } = 0;
        public int Take { get; set; } = 5;  
    }
}
