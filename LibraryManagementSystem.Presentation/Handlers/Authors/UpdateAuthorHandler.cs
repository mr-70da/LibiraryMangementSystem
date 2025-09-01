using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using LibraryManagementSystem.Application.Commands.Authors;
using LibraryManagementSystem.Application.DTOs;
using LibraryManagementSystem.Domain.Entities;
using LibraryManagementSystem.Domain.UnitOfWork;
using MediatR;

namespace LibraryManagementSystem.Application.Handlers.Authors
{
    internal class UpdateAuthorHandler : IRequestHandler<UpdateAuthorCommand, GeneralResponse<AuthorReadDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public UpdateAuthorHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<GeneralResponse<AuthorReadDto>> Handle(UpdateAuthorCommand request, CancellationToken cancellationToken)
        {
            try
            {
                GeneralResponse<AuthorReadDto> response;
                var updateAuthorDto = request.UpdatedAuthorDto;
                if (await _unitOfWork.Authors.GetByIdAsync(updateAuthorDto.Id) == null)
                {
                    response =
                        new GeneralResponse<AuthorReadDto>
                        (null, false, "Author Not Found.", HttpStatusCode.BadRequest);

                }

                Author updatedAuthor = _mapper.Map<Author>(updateAuthorDto);

                _unitOfWork.Authors.Update(updatedAuthor);
                await _unitOfWork.Complete();
                response = new GeneralResponse<AuthorReadDto>
                    (_mapper.Map<AuthorReadDto>(updatedAuthor),
                    true, "Author updated successfully.", HttpStatusCode.OK);
                return response;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while updating the author.", ex);
            }

        }
    }
}
