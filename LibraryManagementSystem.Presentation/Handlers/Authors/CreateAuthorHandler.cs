using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryManagementSystem.Application.Commands.Authors;
using LibraryManagementSystem.Application.DTOs;
using LibraryManagementSystem.Domain.Entities;
using LibraryManagementSystem.Domain.UnitOfWork;
using MediatR;

namespace LibraryManagementSystem.Application.Handlers.Authors
{
    public class CreateAuthorHandler :
        IRequestHandler<CreateAuthorCommand, GeneralResponse<AuthorReadDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly AutoMapper.IMapper _mapper;
        public CreateAuthorHandler(IUnitOfWork unitOfWork, AutoMapper.IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<GeneralResponse<AuthorReadDto>> Handle(CreateAuthorCommand request, CancellationToken cancellationToken)
        {
            try
            {
                GeneralResponse<AuthorReadDto> response;


                var author = _mapper.Map<Author>(request.AuthorDto);
                await _unitOfWork.Authors.AddAsync(author);
                await _unitOfWork.Complete();
                response = new GeneralResponse<AuthorReadDto>
                    (_mapper.Map<AuthorReadDto>(author),
                    true, "Author created successfully.", System.Net.HttpStatusCode.Created);
                return response;

            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while creating the author.", ex);
            }
        }
    }
}
