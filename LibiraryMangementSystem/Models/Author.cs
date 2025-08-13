using System;
using System.Collections.Generic;

namespace LibraryManagementSystem.Models;

public partial class Author
{
    public int AuthorId { get; }

    public string AuthorFname { get; set; } = null!;

    public string? AuthorLname { get; set; }

    public virtual ICollection<Book> BookIsbns { get; } = new List<Book>();
}
