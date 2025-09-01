using AutoMapper;
using LibraryManagementSystem.API.Queries;
using LibraryManagementSystem.Application.DTOs;
using LibraryManagementSystem.Domain.UnitOfWork;
using MediatR;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;

namespace LibraryManagementSystem.API.Handler
{
    public class GetAllUsersHandler : IRequestHandler<GetAllUsersQuery, GeneralResponse<List<UserReadDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetAllUsersHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public IUnitOfWork Get_unitOfWork()
        {
            return _unitOfWork;
        }

        public async Task<GeneralResponse<List<UserReadDto>>> 
            Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
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
    
    }
}
