
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
                throw new Exception("Author not found");
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
                throw new Exception("Author not found");
            }
            return _mapper.Map<AuthorReadDto>(author);
        }
        //Checked
        public async Task UpdateAsync(int id, AuthorCreateDto dto)
        {
            if (await _unitOfWork.Authors.GetByIdAsync(id) == null)
            {
                throw new Exception("Author not found");
            }
            Author updatedAuthor =  _mapper.Map<Author>(dto);
            updatedAuthor.Id = id;
            _unitOfWork.Authors.Update(updatedAuthor);
            await _unitOfWork.Complete();
        }
    }
}
