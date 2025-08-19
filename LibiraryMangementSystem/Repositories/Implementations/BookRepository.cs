using LibraryManagementSystem.Data;
using LibraryManagementSystem.Dtos.Book;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.Repositories.Implementation;
using Microsoft.EntityFrameworkCore.Query;

namespace LibraryManagementSystem.Repositories
{
    public class BookRepository : GenricRepository<Book>, IBookRepository
    {
        public BookRepository(LibraryContext context) : base(context)
        {
        }
        public Book GetByIsbn(string isbn)
        {
            return LibraryContext.Books.Find(isbn);
        }

        public List<Book> GetByAuthor(int authorId)
        {
            List<Book> returnedBooks = LibraryContext.Books
                .Where(b => b.Authors.Any(a => a.Id == authorId)).ToList();
           
            
            return returnedBooks;
        }

        public void Update(int id, Book updatedBook)
        {
            var book = LibraryContext.Books.Find(id);
            updatedBook.Isbn = book.Isbn; // Ensure the ISBN remains the same
            LibraryContext.Entry(book).CurrentValues.SetValues(updatedBook);

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
