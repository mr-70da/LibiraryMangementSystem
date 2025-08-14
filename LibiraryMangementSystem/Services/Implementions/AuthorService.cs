using AutoMapper;
using LibraryManagementSystem.Dtos.Author;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.UnitOfWork;

namespace LibraryManagementSystem.Services
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

        

        public void Create(AuthorCreateDto dto)
        {
            var author = _mapper.Map<Author>(dto);
            _unitOfWork.Authors.Add(author);
            _unitOfWork.Complete();
            
        }

        public void Delete(int  id)
        {
            var author = _unitOfWork.Authors.Get(id);
            _unitOfWork.Authors.Remove(author);
            _unitOfWork.Complete();

        }

        
        public AuthorReadDto Get(int id)
        {
            return _mapper.Map<AuthorReadDto>(_unitOfWork.Authors.Get(id));
        }

        public void Update(int id, AuthorCreateDto dto)
        {
            var author = _unitOfWork.Authors.Get(id);
            var updatedAuthor = _mapper.Map<Author>(dto);
            _unitOfWork.Authors.Update(id,updatedAuthor);
            _unitOfWork.Complete();
        }
    }
}
