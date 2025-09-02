using AutoMapper;
using LibraryManagementSystem.Application.DTOs;
using LibraryManagementSystem.Application.Queries.Books;
using LibraryManagementSystem.Domain.UnitOfWork;
using MediatR;
using Microsoft.Extensions.Logging;
//done
namespace LibraryManagementSystem.Application.Handlers.Books
{
    public class GetBooksCountPerBranchHandler : 
        IRequestHandler<GetBooksCountPerBranchQuery, GeneralResponse<List<BooksPerBranchResponse>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<GetBooksCountPerBranchHandler> _logger;
        public GetBooksCountPerBranchHandler(IUnitOfWork unitOfWork, IMapper mapper , 
            ILogger<GetBooksCountPerBranchHandler> loggr)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = loggr;
        }

        public async Task<GeneralResponse<List<BooksPerBranchResponse>>> 
            Handle(GetBooksCountPerBranchQuery request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Retrieving books count per branch.");
                GeneralResponse<List<BooksPerBranchResponse>> response;
                var result = (from b in await _unitOfWork.Books.GetAllAsync()
                              join l in await _unitOfWork.Branches.GetAllAsync()
                                  on b.BranchId equals l.Id
                              group b by new { b.BranchId, l.BranchName } into g
                              select new BooksPerBranchResponse((int)g.Key.BranchId, g.Key.BranchName, g.Count())
                          ).ToList();
                _logger.LogInformation("Books count per branch retrieved successfully.");
                response = new GeneralResponse<List<BooksPerBranchResponse>>
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
