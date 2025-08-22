using System;
using System.Collections.Generic;

namespace LibraryManagementSystem.Domain.Entities;

public partial class LibraryBranch
{
    public int Id { get; set; }

    public string? BranchName { get; set; }

    public string? Address { get; set; }

    public string? ContactNumber { get; set; }

    public virtual ICollection<Book> Books { get; set; } = new List<Book>();
}
