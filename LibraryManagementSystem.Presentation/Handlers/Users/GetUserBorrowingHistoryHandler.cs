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
using Microsoft.Extensions.Logging;
//done
namespace LibraryManagementSystem.Application.Handlers.Users
{
    internal class GetUserBorrowingHistoryHandler :
        IRequestHandler<GetUserBorrowingHistoryQuery, GeneralResponse<List<UserBorrowingHistoryResponse>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<GetUserBorrowingHistoryHandler> _logger;
        public GetUserBorrowingHistoryHandler(IUnitOfWork unitOfWork, IMapper mapper , 
            ILogger<GetUserBorrowingHistoryHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<GeneralResponse<List<UserBorrowingHistoryResponse>>>
            Handle(GetUserBorrowingHistoryQuery request, CancellationToken cancellationToken)
        {
            try
            {
                GeneralResponse<List<UserBorrowingHistoryResponse>> response;
                if (_unitOfWork.Users.GetByIdAsync(request.UserId) == null)
                {
                    _logger.LogWarning("Attempted to retrieve borrowing history for non-existent user with ID: {UserId}", request.UserId);
                    response = new GeneralResponse<List<UserBorrowingHistoryResponse>>
                        (null, false, "User not found.", System.Net.HttpStatusCode.NotFound);
                    return response;

                }
                var borrowingHistory = await _unitOfWork.Users.GetBorrowingHistoryAsync(request.UserId);
                response = new GeneralResponse<List<UserBorrowingHistoryResponse>>
                    (_mapper.Map<List<UserBorrowingHistoryResponse>>(borrowingHistory),
                    true, "Borrowing history retrieved successfully.", System.Net.HttpStatusCode.OK);
                _logger.LogInformation("Borrowing history retrieved successfully for user with ID: {UserId}", request.UserId);
                return response;

            }
            catch (Exception ex)
            {
               
                throw new Exception(ex.Message);
               
            }
        }
    }
}
