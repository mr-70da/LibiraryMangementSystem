namespace LibraryManagementSystem.Dtos.Book
{
    public class BooksByAuthorDto
    {
        public BooksByAuthorDto(int autherId,List<BookCreateDto> bookCreateDtos,int count) {
            autherId = autherId;
            Books = bookCreateDtos;
            BooksCount = count;

        }
        public int AuthorId { get; set; }
        public int BooksCount { get; set; }
        public List<BookCreateDto> Books { get; set; } = new List<BookCreateDto>();
        
    }
}
