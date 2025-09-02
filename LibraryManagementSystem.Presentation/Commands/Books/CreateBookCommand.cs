using MediatR;
using LibraryManagementSystem.Application.DTOs;

namespace LibraryManagementSystem.Application.Commands.Books
{
    public class CreateBookCommand : IRequest<GeneralResponse<BookReadResponse>>
    {
        
        public int AuthorId { get; set; }
        public string Title { get; set; }
        public int Edition { get; set; }
        public short? CopyRightYear { get; set; }
        public decimal Price { get; set; }

        public CreateBookCommand
            ( int authorId, string title, int edition, short? copyRightYear, decimal price)
        {
            AuthorId = authorId;
            Title = title;
            Edition = edition;
            CopyRightYear = copyRightYear;
            Price = price;
        }

    }
}