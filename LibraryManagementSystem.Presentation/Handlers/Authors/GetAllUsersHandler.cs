using AutoMapper;
using LibraryManagementSystem.Application.DTOs;
using LibraryManagementSystem.Application.Queries.Users;
using LibraryManagementSystem.Domain.UnitOfWork;
using MediatR;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using Microsoft.Extensions.Logging;
//done
namespace LibraryManagementSystem.Application.Handlers.Authors
{
    public class GetAllUsersHandler : IRequestHandler<GetAllUsersQuery, GeneralResponse<List<UserReadResponse>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<GetAllUsersHandler> _logger;
        public GetAllUsersHandler(IUnitOfWork unitOfWork, IMapper mapper , ILogger<GetAllUsersHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }


        public async Task<GeneralResponse<List<UserReadResponse>>> 
            Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            try
            {
                GeneralResponse<List<UserReadResponse>> response;


                var users = await _unitOfWork.Users.GetAllAsync();
                if (users == null)
                {
                    _logger.LogWarning("No users found in the system.");
                    response = new GeneralResponse<List<UserReadResponse>>
                        (null, false, "No users found.", System.Net.HttpStatusCode.NotFound);
                    return response;
                }
                response = new GeneralResponse<List<UserReadResponse>>
                    (_mapper.Map<List<UserReadResponse>>(users),
                    true, "Users retrieved successfully.", System.Net.HttpStatusCode.OK);
                _logger.LogInformation("Users retrieved successfully.");
                return response;

            }
            catch (Exception ex)
            {
                
                throw new Exception("An error occurred while retrieving users."+ex.Message);
            }
        }
    
    }
}
