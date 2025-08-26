
using LibraryManagementSystem.Application.DTOs;

namespace LibraryManagementSystem.Domain.Interfaces.Services
{
    public interface IAuthorService
    {

        public Task CreateAsync(AuthorCreateDto dto);
        public Task<AuthorReadDto> GetAsync(int id);
        public Task DeleteAsync(int id);
        public Task UpdateAsync(int id,AuthorCreateDto dto);
    }
}
