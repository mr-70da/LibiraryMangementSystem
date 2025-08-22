
using AutoMapper;
using LibraryManagementSystem.Application.DTOs;
using LibraryManagementSystem.Application.Services;
using LibraryManagementSystem.Domain.UnitOfWork;

namespace LibraryManagementSystem.Infrastructure.Services
{
    internal class UserService : IUserService
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
            throw new NotImplementedException();
        }

        public List<UserReadDto> GetAll()
        {
            throw new NotImplementedException();
        }

        public List<UserBorrowingHistoryDto> GetBorrowingHistory(int userId)
        {
            throw new NotImplementedException();
        }
    }
}
