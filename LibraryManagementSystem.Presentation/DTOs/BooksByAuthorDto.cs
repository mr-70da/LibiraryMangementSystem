namespace LibraryManagementSystem.Application.DTOs
{
    public class BooksByAuthorDto
    {
        public BooksByAuthorDto(
            int authorId,
            List<BookWithoutAuthorDto> bookDto,
            int count
           )      {
            AuthorId = authorId;
            Books = bookDto;
            BooksCount = count;
            
        }

        public int AuthorId { get; set; }
        public int BooksCount { get; set; }
        public List<BookWithoutAuthorDto> Books { get; set; } = new List<BookWithoutAuthorDto>();

   
    }
}
