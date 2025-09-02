using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryManagementSystem.Application.DTOs;
using MediatR;
//done
namespace LibraryManagementSystem.Application.Queries.Users
{
    public class GetUserBorrowingHistoryQuery :IRequest<GeneralResponse<List<UserBorrowingHistoryResponse>>>
    {
        public int UserId { get; }
        public GetUserBorrowingHistoryQuery(int userId)
        {
            UserId = userId;
        }
    }
}
