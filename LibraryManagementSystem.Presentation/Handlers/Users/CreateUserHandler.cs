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

namespace LibraryManagementSystem.Application.Handlers.Users
{
    internal class CreateUserHandler : IRequestHandler<CreateUserCommand, GeneralResponse<UserReadDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CreateUserHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<GeneralResponse<UserReadDto>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var userCreateDto = request.CreateUserDto;
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
    }
}
