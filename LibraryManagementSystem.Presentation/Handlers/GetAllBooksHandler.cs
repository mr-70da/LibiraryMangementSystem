using AutoMapper;
using LibraryManagementSystem.API.Queries;
using LibraryManagementSystem.Application.DTOs;
using LibraryManagementSystem.Domain.UnitOfWork;
using MediatR;

namespace LibraryManagementSystem.API.Handler
{
    public class GetAllBooksHandler : IRequestHandler<GetAllBooksQuery, GeneralResponse<List<BooksFilterResponse>>>
    {


        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetAllBooksHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        
        public Task<GeneralResponse<List<BooksFilterResponse>>> Handle(GetAllBooksQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
