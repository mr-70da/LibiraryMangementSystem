using AutoMapper;
using LibraryManagementSystem.Application.Commands.Authors;
using LibraryManagementSystem.Application.DTOs;
using LibraryManagementSystem.Domain.UnitOfWork;
using MediatR;

namespace LibraryManagementSystem.Application.Handlers.Authors
{
    internal class DeleteAuthorHandler : IRequestHandler<DeleteAuthorCommand, GeneralResponse<AuthorReadDto>>
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public DeleteAuthorHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<GeneralResponse<AuthorReadDto>> 
            Handle(DeleteAuthorCommand request, CancellationToken cancellationToken)
        {

            GeneralResponse<AuthorReadDto> response;
            var author = await _unitOfWork.Authors.GetByIdAsync(request.Id);
            if (author == null)
            {
                response = new GeneralResponse<AuthorReadDto>
                   (null, false, "Author not found.", System.Net.HttpStatusCode.NotFound);

            }

            _unitOfWork.Authors.Remove(author);
            await _unitOfWork.Complete();
            return response = new GeneralResponse<AuthorReadDto>
                   (_mapper.Map<AuthorReadDto>(author),
                   true, "Author deleted successfully.", System.Net.HttpStatusCode.OK);
        }
    }
}
