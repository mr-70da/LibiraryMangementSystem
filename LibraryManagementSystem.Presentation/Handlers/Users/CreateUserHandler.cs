using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using LibraryManagementSystem.Application.Commands.Users;
using LibraryManagementSystem.Application.DTOs;
using LibraryManagementSystem.Domain.Entities;
using LibraryManagementSystem.Domain.UnitOfWork;
using MediatR;
using Microsoft.Extensions.Logging;

namespace LibraryManagementSystem.Application.Handlers.Users
{
    internal class CreateUserHandler : IRequestHandler<CreateUserCommand, GeneralResponse<UserReadResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateUserHandler> _logger;
        public CreateUserHandler(IUnitOfWork unitOfWork, IMapper mapper,ILogger<CreateUserHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<GeneralResponse<UserReadResponse>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var userCreateDto = request;
                GeneralResponse<UserReadResponse> response;
                await _unitOfWork.Users.AddAsync(_mapper.Map<User>(userCreateDto));
                await _unitOfWork.Complete();
                response = new GeneralResponse<UserReadResponse>
                    (_mapper.Map<UserReadResponse>(userCreateDto), true, "User created successfully.", System.Net.HttpStatusCode.OK);
                _logger.LogInformation("User created successfully with Email: {userCreateDto.Email}", userCreateDto.Email);
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating the user.");
                throw new Exception(ex.Message);

            }
        }
    }
}
