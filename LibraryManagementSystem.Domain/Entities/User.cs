using System;
using System.Collections.Generic;

namespace LibraryManagementSystem.Domain.Entities;

public partial class User
{
    public int Id { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string Email { get; set; } = null!;

    public DateOnly? RegistrationDate { get; set; }

    public virtual ICollection<BorrowingHistory> BorrowingHistories { get; set; } = new List<BorrowingHistory>();
}
