using System;
using System.Collections.Generic;

namespace LibraryManagementSystem.Domain.Entities;

public partial class Author
{
    public int Id { get; set; }

    public string FirstName { get; set; } = null!;

    public string? LastName { get; set; }

    public virtual ICollection<Book> Books { get; set; } = new List<Book>();
}
