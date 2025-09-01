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
//done
namespace LibraryManagementSystem.Application.Handlers.Users
{
    internal class GetAuthorByIdHandler : IRequestHandler<GetAuthorByIdQuery, GeneralResponse<AuthorReadDto>>

    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetAuthorByIdHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<GeneralResponse<AuthorReadDto>> Handle(GetAuthorByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                GeneralResponse<AuthorReadDto> response;
                var author = await _unitOfWork.Authors.GetByIdAsync(request.Id);
                if (author == null)
                {

                    response =
                        new GeneralResponse<AuthorReadDto>
                        (null, false, "Author Not Found.", HttpStatusCode.BadRequest);
                    throw new KeyNotFoundException("Author not found");
                }
                response = new GeneralResponse<AuthorReadDto>
                    (_mapper.Map<AuthorReadDto>(author),
                    true, "Author retrieved successfully.", HttpStatusCode.OK);
                return response;

            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving the author.", ex);
            }
        }
    }
}
