using AutoMapper;
using LibraryManagementSystem.Domain.UnitOfWork;

namespace LibraryManagementSystem.API.Handler
{
    public class GetBooksCountPerBranchHandler
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetBooksCountPerBranchHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
    }
}
