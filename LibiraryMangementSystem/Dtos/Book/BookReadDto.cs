namespace LibraryManagementSystem.Dtos.Book
{
    public class BookReadDto
    {
        public int Isbn { get; set; }
        public List<string> AuthorNames { get; set; } = new();
    }
}
