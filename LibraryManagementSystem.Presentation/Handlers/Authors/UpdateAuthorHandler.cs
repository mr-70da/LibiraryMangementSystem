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
using Microsoft.Extensions.Logging;

namespace LibraryManagementSystem.Application.Handlers.Authors
{
    internal class UpdateAuthorHandler : IRequestHandler<UpdateAuthorCommand, GeneralResponse<AuthorReadResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateAuthorHandler> _logger;
        public UpdateAuthorHandler(IUnitOfWork unitOfWork, IMapper mapper ,ILogger<UpdateAuthorHandler> logger)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<GeneralResponse<AuthorReadResponse>> 
            Handle(UpdateAuthorCommand request, CancellationToken cancellationToken)
        {
            try
            {
                GeneralResponse<AuthorReadResponse> response;
                var updateAuthorDto = request;
                if (await _unitOfWork.Authors.GetByIdAsync(updateAuthorDto.Id) == null)
                {
                    _logger.LogInformation("Attempted to update non-existent author with ID: {AuthorId}", updateAuthorDto.Id);
                    response =
                        new GeneralResponse<AuthorReadResponse>
                        (null, false, "Author Not Found.", HttpStatusCode.BadRequest);

                }

                Author updatedAuthor = _mapper.Map<Author>(updateAuthorDto);

                _unitOfWork.Authors.Update(updatedAuthor);
                await _unitOfWork.Complete();
                _logger.LogInformation("Author updated successfully with ID: {AuthorId}", updatedAuthor.Id);
                response = new GeneralResponse<AuthorReadResponse>
                    (_mapper.Map<AuthorReadResponse>(updatedAuthor),
                    true, "Author updated successfully.", HttpStatusCode.OK);
                return response;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
    }
}
