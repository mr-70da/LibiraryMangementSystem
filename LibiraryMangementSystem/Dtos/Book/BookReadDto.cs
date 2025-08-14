namespace LibraryManagementSystem.Dtos.Book
{
    public class BookReadDto
    {
        public int Isbn { get; set; }
        public string Title { get; set; }
        public int Edition { get; set; }
        public short? CopyRightYear { get; set; }
        public decimal Price { get; set; }
        public List<string> AuthorNames { get; set; } = new();
    }
}
