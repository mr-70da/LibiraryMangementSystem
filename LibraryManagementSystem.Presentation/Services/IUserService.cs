using LibraryManagementSystem.Application.DTOs;
using LibraryManagementSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Application.Services
{
    public interface IUserService
    {
        public Task CreateAsync(UserCreateDto userCreateDto);
        public Task <List<UserBorrowingHistoryDto>> GetBorrowingHistoryAsync(int userId);
        public Task<List<UserReadDto>> GetAllAsync();
    
    }
}
