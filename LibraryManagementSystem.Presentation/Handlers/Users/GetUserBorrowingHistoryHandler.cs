using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using LibraryManagementSystem.Application.DTOs;
using LibraryManagementSystem.Application.Queries.Users;
using LibraryManagementSystem.Domain.Entities;
using LibraryManagementSystem.Domain.UnitOfWork;
using MediatR;
//done
namespace LibraryManagementSystem.Application.Handlers.Users
{
    internal class GetUserBorrowingHistoryHandler :
        IRequestHandler<GetUserBorrowingHistoryQuery, GeneralResponse<List<UserBorrowingHistoryDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetUserBorrowingHistoryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<GeneralResponse<List<UserBorrowingHistoryDto>>>
            Handle(GetUserBorrowingHistoryQuery request, CancellationToken cancellationToken)
        {
            try
            {
                GeneralResponse<List<UserBorrowingHistoryDto>> response;
                if (_unitOfWork.Users.GetByIdAsync(request.UserId) == null)
                {
                    response = new GeneralResponse<List<UserBorrowingHistoryDto>>
                        (null, false, "User not found.", System.Net.HttpStatusCode.NotFound);
                    return response;

                }
                var borrowingHistory = await _unitOfWork.Users.GetBorrowingHistoryAsync(request.UserId);
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
