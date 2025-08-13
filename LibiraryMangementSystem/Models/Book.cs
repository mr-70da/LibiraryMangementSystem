using System;
using System.Collections.Generic;

namespace LibraryManagementSystem.Models;

public partial class Book
{
    public int BookIsbn { get; }

    public string BookTitle { get; set; } = null!;

    public int BookEdition { get; set; }

    public short? BookCopyRight { get; set; }

    public decimal Price { get; set; }

    public virtual ICollection<Author> Authors { get; } = new List<Author>();
}
