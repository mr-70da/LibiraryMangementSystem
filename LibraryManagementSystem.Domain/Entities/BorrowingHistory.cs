namespace LibraryManagementSystem.Domain.Entities;

public partial class BorrowingHistory
{
    public int Id { get; set; }

    public int? BookId { get; set; }

    public int? UserId { get; set; }

    public DateOnly BorrowDate { get; set; }

    public DateOnly? ReturnDate { get; set; }

    public virtual Book? Book { get; set; }

    public virtual User? User { get; set; }
}
