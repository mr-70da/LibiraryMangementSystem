using AutoMapper;
using LibraryManagementSystem.Application.Commands.Authors;
using LibraryManagementSystem.Application.DTOs;
using LibraryManagementSystem.Domain.UnitOfWork;
using MediatR;
using Microsoft.Extensions.Logging;

namespace LibraryManagementSystem.Application.Handlers.Authors
{
    internal class DeleteAuthorHandler : IRequestHandler<DeleteAuthorCommand, GeneralResponse<AuthorReadResponse>>
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<DeleteAuthorHandler> _logger;
        public DeleteAuthorHandler(IUnitOfWork unitOfWork, IMapper mapper , ILogger<DeleteAuthorHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<GeneralResponse<AuthorReadResponse>>
            Handle(DeleteAuthorCommand request, CancellationToken cancellationToken)
        {

            try
            {
                GeneralResponse<AuthorReadResponse> response;
                var author = await _unitOfWork.Authors.GetByIdAsync(request.Id);
                if (author == null)
                {
                    _logger.LogWarning("Attempted to delete non-existent author with ID: {AuthorId}", request.Id);
                    response = new GeneralResponse<AuthorReadResponse>
                       (null, false, "Author not found.", System.Net.HttpStatusCode.NotFound);

                }

                _unitOfWork.Authors.Remove(author);
                await _unitOfWork.Complete();
                _logger.LogInformation("Author deleted successfully with ID: {AuthorId}", request.Id);
                return response = new GeneralResponse<AuthorReadResponse>
                       (_mapper.Map<AuthorReadResponse>(author),
                       true, "Author deleted successfully.", System.Net.HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
