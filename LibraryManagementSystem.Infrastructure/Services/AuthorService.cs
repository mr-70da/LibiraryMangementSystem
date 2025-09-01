
using AutoMapper;
using LibraryManagementSystem.Application.DTOs;
using LibraryManagementSystem.Domain.Entities;
using LibraryManagementSystem.Domain.Interfaces.Services;
using LibraryManagementSystem.Domain.UnitOfWork;


namespace LibraryManagementSystem.Application.Interfaces.Services
{
    public class AuthorService :IAuthorService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AuthorService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        
        //Checked
        public async Task CreateAsync(AuthorCreateDto dto)
        {
            var author = _mapper.Map<Author>(dto);
            await _unitOfWork.Authors.AddAsync(author);
            await _unitOfWork.Complete();
            
        }
        //Checked
        public async Task DeleteAsync(int id)
        {
            var author = await _unitOfWork.Authors.GetByIdAsync(id);
            if (author == null)
            {
                throw new KeyNotFoundException("Author not found");
            }
            _unitOfWork.Authors.Remove(author);
            await _unitOfWork.Complete();

        }

        //Checked
        public async Task<AuthorReadDto> GetAsync(int id)
        {
            var author = await _unitOfWork.Authors.GetByIdAsync(id);
            if (author == null)
            {
                throw new KeyNotFoundException("Author not found");
            }
            return _mapper.Map<AuthorReadDto>(author);
        }
        //Checked
        public async Task UpdateAsync(AuthorUpdateRequestDto updateAuthorDto)
        {
            if (await _unitOfWork.Authors.GetByIdAsync(updateAuthorDto.Id) == null)
            {
                throw new KeyNotFoundException("Author not found"); 
            }

            Author updatedAuthor =  _mapper.Map<Author>(updateAuthorDto);
            
            _unitOfWork.Authors.Update(updatedAuthor);
            await _unitOfWork.Complete();
        }
    }
}
