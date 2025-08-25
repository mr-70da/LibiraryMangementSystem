
namespace LibraryManagementSystem.Application.DTOs
{
    public class BooksPerBranchDto
    {
        public BooksPerBranchDto(int id, String name, int count) {
            BranchId = id; BranchName = name; BookCount = count;
        }
        public int BranchId { get; set; }
        public string BranchName { get; set; }
        public int BookCount { get; set; }
    }
}
