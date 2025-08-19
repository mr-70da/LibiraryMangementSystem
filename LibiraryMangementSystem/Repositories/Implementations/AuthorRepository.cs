using LibraryManagementSystem.Data;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.Repositories.Implementation;

namespace LibraryManagementSystem.Repositories
{
    public class AuthorRepository : GenricRepository<Author>, IAuthorRepository
    {
        public AuthorRepository(LibraryContext context) : base(context)
        {
        }
        public Author Get(int id)
        {
            return LibraryContext.Authors.Find(id);
        }


        public void Update(int id, Author updatedAuthor)
        {
            var existingAuthor = LibraryContext.Authors.Find(id);
            updatedAuthor.Id = existingAuthor.Id;
            if (existingAuthor == null)
                throw new InvalidOperationException($"Author with id {id} not found.");

            LibraryContext.Entry(existingAuthor).CurrentValues.SetValues(updatedAuthor);

            LibraryContext.SaveChanges();
            
        }
        public void Add(Author author)
        {
            LibraryContext.Authors.Add(author);
            LibraryContext.SaveChanges();
        }
        public void Remove(Author author) 
        {
            LibraryContext.Authors.Remove(author);
            LibraryContext.SaveChanges();
        }
        public LibraryContext LibraryContext
        {
            get { return Context as LibraryContext; }
        }
    }
   
}
