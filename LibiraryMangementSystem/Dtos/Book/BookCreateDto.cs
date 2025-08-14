namespace LibraryManagementSystem.Dtos.Book

{
    public class BookCreateDto
    {
        public string Title { get; set; }
        public int Edition { get; set; }
        public short? CopyRightYear { get; set; }
        public decimal Price { get; set; }

       
        public List<int> AuthorIds { get; set; } = new();
    }
}
