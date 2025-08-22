
using LibraryManagementSystem.Application.DTOs;

namespace LibraryManagementSystem.Domain.Interfaces.Services
{
    public interface IAuthorService
    {

        public void Create(AuthorCreateDto dto);
        public AuthorReadDto Get(int id);
        public void Delete(int id);
        public void Update(int id,AuthorCreateDto dto);
    }
}
