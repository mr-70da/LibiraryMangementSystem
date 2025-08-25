namespace LibraryManagementSystem.Application.DTOs
{
    public class BookFilterDto
    {
       
        public int? AuthorId { get; set; } 
        public string? BookName { get; set; }
        public int? BranchId { get; set; }

    
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;  
    }
}
