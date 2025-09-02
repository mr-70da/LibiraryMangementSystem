using LibraryManagementSystem.Application.DTOs;
using MediatR;
//done
namespace LibraryManagementSystem.Application.Queries.Books
{
    public class GetAllBooksQuery : IRequest<GeneralResponse<BooksFilterResponse>>
    {
     
        public int? AuthorId { get; set; }
        public string? BookName { get; set; }
        public int? BranchId { get; set; }


        public int Skip { get; set; } = 0;
        public int Take { get; set; } = 5;


        public GetAllBooksQuery(int? authorId, string? bookName, int? branchId, int skip, int take)
        {
            AuthorId = authorId;
            BookName = bookName;
            BranchId = branchId;
            Skip = skip;
            Take = take;
        }

        public GetAllBooksQuery()
        {
        }
    }
}
