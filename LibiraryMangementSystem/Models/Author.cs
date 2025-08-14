using System;
using System.Collections.Generic;

namespace LibraryManagementSystem.Models;

public partial class Author
{
    public int Id { get; set; }

    public string FirstName { get; set; } = null!;

    public string? LastName { get; set; }

    public virtual ICollection<Book> BookIsbns { get; set; } = new List<Book>();
}
