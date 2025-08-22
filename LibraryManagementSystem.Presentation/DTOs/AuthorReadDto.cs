namespace LibraryManagementSystem.Application.DTOs
{
    public class AuthorReadDto
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public List<string> BookTitles { get; set; } = new();
    }
}
