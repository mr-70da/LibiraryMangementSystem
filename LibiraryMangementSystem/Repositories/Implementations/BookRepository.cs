using LibraryManagementSystem.Data;
using LibraryManagementSystem.Dtos.Book;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.Repositories.Implementation;

namespace LibraryManagementSystem.Repositories
{
    public class BookRepository : Repository<Book>, IBookRepository
    {
        public BookRepository(LibraryContext context) : base(context)
        {
        }
        public Book GetByIsbn(string isbn)
        {
            return LibraryContext.Books.Find(isbn);
        }

        public IEnumerable<Book> GetByAuthor(int authorId)
        {
            return LibraryContext.Books
                .Where(b => b.Authors.Any(a => a.Id == authorId))
                .ToList();
            
        }

        public void Update(int id, Book updatedBook)
        {
            var book = LibraryContext.Books.Find(id);
            updatedBook.Isbn = book.Isbn; // Ensure the ISBN remains the same
            book = updatedBook;
            LibraryContext.Books.Update(book);
            LibraryContext.SaveChanges();

        }
        public void Create(Author author ,Book book)
        {
            book.Authors.Add(author);
            LibraryContext.Books.Add(book);
            LibraryContext.SaveChanges();
        }

        public LibraryContext LibraryContext
        {
            get { return Context as LibraryContext; }
        }
    }
}
