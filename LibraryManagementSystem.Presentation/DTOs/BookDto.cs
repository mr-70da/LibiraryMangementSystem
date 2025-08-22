namespace LibraryManagementSystem.Application.DTOs
{
    public abstract class BookDto
    {
        public string Title { get; set; }
        public int Edition { get; set; }
        public short? CopyRightYear { get; set; }
        public decimal Price { get; set; }
    }
}
