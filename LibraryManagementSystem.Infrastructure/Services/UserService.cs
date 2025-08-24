
using AutoMapper;
using LibraryManagementSystem.Application.DTOs;
using LibraryManagementSystem.Application.Services;
using LibraryManagementSystem.Domain.Entities;
using LibraryManagementSystem.Domain.UnitOfWork;

namespace LibraryManagementSystem.Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public UserService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public void Create(UserCreateDto userCreateDto)
        {
            
            _unitOfWork.Users.Add(_mapper.Map<User>(userCreateDto));
            _unitOfWork.Complete();
        }

        public List<UserReadDto> GetAll()
        {
            var users = _unitOfWork.Users.GetAll().ToList();
            return _mapper.Map<List<UserReadDto>>(users);
        }

        public List<UserBorrowingHistoryDto> GetBorrowingHistory(int userId)
        {
            var borrowingHistory = _unitOfWork.Users.GetBorrowingHistory(userId).ToList();
            return _mapper.Map<List<UserBorrowingHistoryDto>>(borrowingHistory);
        }
    }
}
