
using AutoMapper;
using LibraryManagementSystem.Application.DTOs;
using LibraryManagementSystem.Application.Services.Interface;
using LibraryManagementSystem.Domain.Entities;
using LibraryManagementSystem.Domain.UnitOfWork;

namespace LibraryManagementSystem.Application.Services.Implementations
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

        public async Task<GeneralResponse<UserReadDto>> CreateAsync(UserCreateDto userCreateDto)
        {
            try
            {
                GeneralResponse<UserReadDto> response;
                await _unitOfWork.Users.AddAsync(_mapper.Map<User>(userCreateDto));
                await _unitOfWork.Complete();
                response = new GeneralResponse<UserReadDto>
                    (_mapper.Map<UserReadDto>(userCreateDto), true, "User created successfully.", System.Net.HttpStatusCode.OK);
                return response;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);

            }
        }

        public async Task<GeneralResponse <List<UserReadDto>>> GetAllAsync()
        {
            try
            {
                GeneralResponse<List<UserReadDto>> response;

                var users = await _unitOfWork.Users.GetAllAsync();
                if (users == null)
                {
                    response = new GeneralResponse<List<UserReadDto>>
                        (null, false, "No users found.", System.Net.HttpStatusCode.NotFound);
                    return response;
                }
                response = new GeneralResponse<List<UserReadDto>>
                    (_mapper.Map<List<UserReadDto>>(users),
                    true, "Users retrieved successfully.", System.Net.HttpStatusCode.OK);
                return response;
       
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<GeneralResponse<List<UserBorrowingHistoryDto>>> GetBorrowingHistoryAsync(int userId)
        {
            try
            {
                GeneralResponse<List<UserBorrowingHistoryDto>> response;
                if (_unitOfWork.Users.GetByIdAsync(userId) == null)
                {
                    response = new GeneralResponse<List<UserBorrowingHistoryDto>>
                        (null, false, "User not found.", System.Net.HttpStatusCode.NotFound);
                    return response;

                }
                var borrowingHistory = await _unitOfWork.Users.GetBorrowingHistoryAsync(userId);
                response = new GeneralResponse<List<UserBorrowingHistoryDto>>
                    (_mapper.Map<List<UserBorrowingHistoryDto>>(borrowingHistory),
                    true, "Borrowing history retrieved successfully.", System.Net.HttpStatusCode.OK);
                return response;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
