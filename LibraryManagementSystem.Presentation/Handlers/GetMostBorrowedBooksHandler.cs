using AutoMapper;
using LibraryManagementSystem.API.Queries;
using LibraryManagementSystem.Application.DTOs;
using LibraryManagementSystem.Domain.UnitOfWork;
using MediatR;

namespace LibraryManagementSystem.API.Handler
{
    public class GetMostBorrowedBooksHandler : IRequestHandler<GetMostBorrowedBooksQuery,
        GeneralResponse<List<MostBorrowedBooksDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetMostBorrowedBooksHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<GeneralResponse<List<MostBorrowedBooksDto>>>
            Handle(GetMostBorrowedBooksQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var books = await _unitOfWork.Books.GetAllAsync();
                var branches = await _unitOfWork.Branches.GetAllAsync();

                var result = (from b in books
                              join l in branches on b.BranchId equals l.Id
                              group b by new { b.BranchId, l.BranchName } into g
                              select new BooksPerBranchDto(
                                  g.Key.BranchId,
                                  g.Key.BranchName,
                                  g.Count()
                              )).ToList();

                return new GeneralResponse<List<BooksPerBranchDto>>(
                    result,
                    true,
                    "Books count per branch retrieved successfully",
                    System.Net.HttpStatusCode.OK
                );
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving books count per branch: " + ex.Message, ex);
            }
        }
    }
}
