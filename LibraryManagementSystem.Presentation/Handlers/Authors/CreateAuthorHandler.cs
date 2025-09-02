using AutoMapper;
using LibraryManagementSystem.Application.Commands.Authors;
using LibraryManagementSystem.Application.DTOs;
using LibraryManagementSystem.Domain.Entities;
using LibraryManagementSystem.Domain.UnitOfWork;
using MediatR;
using Microsoft.Extensions.Logging;

namespace LibraryManagementSystem.Application.Handlers.Authors
{
    public class CreateAuthorHandler :
        IRequestHandler<CreateAuthorCommand, GeneralResponse<AuthorReadResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateAuthorHandler> _logger;

        public CreateAuthorHandler(IUnitOfWork unitOfWork, IMapper mapper , ILogger<CreateAuthorHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<GeneralResponse<AuthorReadResponse>> Handle(CreateAuthorCommand request, CancellationToken cancellationToken)
        {
            try
            {
                GeneralResponse<AuthorReadResponse> response;


                var author = _mapper.Map<Author>(request);
                await _unitOfWork.Authors.AddAsync(author);
                await _unitOfWork.Complete();
                _logger.LogInformation("Author created successfully with ID: {AuthorId}", author.Id);
                response = new GeneralResponse<AuthorReadResponse>
                    (_mapper.Map<AuthorReadResponse>(author),
                    true, "Author created successfully.", System.Net.HttpStatusCode.Created);
                return response;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating the author.");
                throw new Exception("An error occurred while creating the author.", ex);
            }
        }
    }
}
