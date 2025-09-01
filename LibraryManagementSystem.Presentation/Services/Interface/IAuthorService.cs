using LibraryManagementSystem.Application.DTOs;

namespace LibraryManagementSystem.Application.Services.Interface
{
    public interface IAuthorService
    {

        public Task<GeneralResponse<AuthorReadDto>> CreateAsync(AuthorCreateDto dto);
        public Task<GeneralResponse<AuthorReadDto>> GetAsync(int id);
        public Task<GeneralResponse<AuthorReadDto>> DeleteAsync(int id);
        public Task<GeneralResponse<AuthorReadDto>> UpdateAsync(AuthorUpdateRequestDto UpdatedAuthorDto);
    }
}
