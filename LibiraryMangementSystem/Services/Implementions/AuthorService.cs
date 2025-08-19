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

        
        //Checked
        public void Create(AuthorCreateDto dto)
        {
            var author = _mapper.Map<Author>(dto);
            _unitOfWork.Authors.Add(author);
            _unitOfWork.Complete();
            
        }
        //Checked
        public void Delete(int id)
        {
            var author = _unitOfWork.Authors.GetById(id);
            _unitOfWork.Authors.Remove(author);
            _unitOfWork.Complete();

        }

        //Checked
        public AuthorReadDto Get(int id)
        {
            return _mapper.Map<AuthorReadDto>(_unitOfWork.Authors.GetById(id));
        }
        //Checked
        public void Update(int id, AuthorCreateDto dto)
        {
            
            Author updatedAuthor = _mapper.Map<Author>(dto);
            updatedAuthor.Id = id;
            _unitOfWork.Authors.Update(updatedAuthor);
            _unitOfWork.Complete();
        }
    }
}
