using LibraryManagementSystem.Application.DTOs;
using LibraryManagementSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Application.Services.Interface
{
    public interface IUserService
    {
        public Task<GeneralResponse<UserReadDto>> CreateAsync(UserCreateDto userCreateDto);
        public Task <GeneralResponse<List<UserBorrowingHistoryDto>>> GetBorrowingHistoryAsync(int userId);
        public Task<GeneralResponse<List<UserReadDto>>> GetAllAsync();
    
    }
}
