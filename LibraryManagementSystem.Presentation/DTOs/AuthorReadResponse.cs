namespace LibraryManagementSystem.Application.DTOs
{
    public class AuthorReadResponse
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public List<string> BookTitles { get; set; } = new();
    }
}
