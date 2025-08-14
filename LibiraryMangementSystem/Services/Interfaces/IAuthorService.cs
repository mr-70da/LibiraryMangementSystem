using AutoMapper;
using LibraryManagementSystem.Dtos.Author;
using LibraryManagementSystem.Dtos.Book;
using LibraryManagementSystem.UnitOfWork;

namespace LibraryManagementSystem.Services
{
    public interface IAuthorService
    {

        public void Create(AuthorCreateDto dto);
        public AuthorReadDto Get(int id);
        public void Delete(int dto);
        public void Update(int id,AuthorCreateDto dto);
    }
}
