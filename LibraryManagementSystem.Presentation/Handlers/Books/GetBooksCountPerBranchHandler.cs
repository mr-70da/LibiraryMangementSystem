using AutoMapper;
using LibraryManagementSystem.Application.DTOs;
using LibraryManagementSystem.Application.Queries.Books;
using LibraryManagementSystem.Domain.UnitOfWork;
using MediatR;
//done
namespace LibraryManagementSystem.Application.Handlers.Books
{
    public class GetBooksCountPerBranchHandler : 
        IRequestHandler<GetBooksCountPerBranchQuery, GeneralResponse<List<BooksPerBranchDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetBooksCountPerBranchHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<GeneralResponse<List<BooksPerBranchDto>>> 
            Handle(GetBooksCountPerBranchQuery request, CancellationToken cancellationToken)
        {
            try
            {
                GeneralResponse<List<BooksPerBranchDto>> response;
                var result = (from b in await _unitOfWork.Books.GetAllAsync()
                              join l in await _unitOfWork.Branches.GetAllAsync()
                                  on b.BranchId equals l.Id
                              group b by new { b.BranchId, l.BranchName } into g
                              select new BooksPerBranchDto((int)g.Key.BranchId, g.Key.BranchName, g.Count())
                          ).ToList();

                response = new GeneralResponse<List<BooksPerBranchDto>>
                    (result, true, "Books count per branch retrieved successfully", System.Net.HttpStatusCode.OK);
                return response;

            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving books count per branch: " + ex.Message);
            }
        }
    }
}
