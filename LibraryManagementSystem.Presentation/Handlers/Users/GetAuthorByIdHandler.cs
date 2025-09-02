using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using LibraryManagementSystem.Application.DTOs;
using LibraryManagementSystem.Application.Queries.Authors;
using LibraryManagementSystem.Domain.UnitOfWork;
using MediatR;
using Microsoft.Extensions.Logging;
//done
namespace LibraryManagementSystem.Application.Handlers.Users
{
    internal class GetAuthorByIdHandler : IRequestHandler<GetAuthorByIdQuery, GeneralResponse<AuthorReadResponse>>

    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<GetAuthorByIdHandler> _logger;
        public GetAuthorByIdHandler(IUnitOfWork unitOfWork, IMapper mapper , ILogger<GetAuthorByIdHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<GeneralResponse<AuthorReadResponse>> Handle(GetAuthorByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                GeneralResponse<AuthorReadResponse> response;
                var author = await _unitOfWork.Authors.GetByIdAsync(request.Id);
                if (author == null)
                {
                    _logger .LogWarning("Attempted to retrieve non-existent author with ID: {AuthorId}", request.Id);
                    response =
                        new GeneralResponse<AuthorReadResponse>
                        (null, false, "Author Not Found.", HttpStatusCode.BadRequest);
                    throw new KeyNotFoundException("Author not found");
                }
                response = new GeneralResponse<AuthorReadResponse>
                    (_mapper.Map<AuthorReadResponse>(author),
                    true, "Author retrieved successfully.", HttpStatusCode.OK);
                _logger.LogInformation("Author retrieved successfully with ID: {AuthorId}", request.Id);
                return response;

            }
            catch (Exception ex)
            {
               
                throw new Exception("An error occurred while retrieving the author.", ex);
            }
        }
    }
}
