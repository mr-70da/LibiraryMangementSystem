using System;
using System.Collections.Generic;

namespace LibraryManagementSystem.Domain.Entities;

public partial class Book
{
    public int Isbn { get; set; }

    public int? AuthorId { get; set; }

    public int? BranchId { get; set; }

    public string Title { get; set; } = null!;

    public int Edition { get; set; }

    public short? CopyRightYear { get; set; }

    public decimal Price { get; set; }

    public virtual Author? Author { get; set; }

    public virtual ICollection<BorrowingHistory> BorrowingHistories { get; set; } = new List<BorrowingHistory>();

    public virtual LibraryBranch? Branch { get; set; }
}
