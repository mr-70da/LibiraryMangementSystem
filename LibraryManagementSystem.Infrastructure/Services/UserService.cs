
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

        public async Task CreateAsync(UserCreateDto userCreateDto)
        {
            
            await _unitOfWork.Users.AddAsync(_mapper.Map<User>(userCreateDto));
            await _unitOfWork.Complete();
        }

        public async Task<List<UserReadDto>> GetAllAsync()
        {
            var users =  await _unitOfWork.Users.GetAllAsync();
            return _mapper.Map<List<UserReadDto>>(users);
        }

        public async Task<List<UserBorrowingHistoryDto>> GetBorrowingHistoryAsync(int userId)
        {
            var borrowingHistory = await _unitOfWork.Users.GetBorrowingHistoryAsync(userId);
            return _mapper.Map<List<UserBorrowingHistoryDto>>(borrowingHistory);
        }
    }
}
