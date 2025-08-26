namespace LibraryManagementSystem.Application.DTOs
{
    public class BookReadDto : BookDto
    {
        public int Isbn { get; set; }
        public int AuthorId { get; set; }
    }
}
