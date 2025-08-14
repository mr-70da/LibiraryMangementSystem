using System;
using System.Collections.Generic;

namespace LibraryManagementSystem.Models;

public partial class Book
{
    public int Isbn { get; set; }

    public string Title { get; set; } = null!;

    public int Edition { get; set; }

    public short? CopyRightYear { get; set; }

    public decimal Price { get; set; }

    public virtual ICollection<Author> Authors { get; set; } = new List<Author>();
}
