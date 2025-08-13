using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.Models;

public partial class LibiraryContext : DbContext
{
    public LibiraryContext()
    {
    }

    public LibiraryContext(DbContextOptions<LibiraryContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Author> Authors { get; set; }

    public virtual DbSet<Book> Books { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-HKCKHUN;Database=Libirary;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Author>(entity =>
        {
            entity.HasKey(e => e.AuthorId).HasName("PK__Author__86516BCF05B27966");

            entity.ToTable("Author");

            entity.Property(e => e.AuthorId).HasColumnName("author_id");
            entity.Property(e => e.AuthorFname)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("author_fname");
            entity.Property(e => e.AuthorLname)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("author_lname");

            entity.HasMany(d => d.BookIsbns).WithMany(p => p.Authors)
                .UsingEntity<Dictionary<string, object>>(
                    "AuthorsOfBook",
                    r => r.HasOne<Book>().WithMany()
                        .HasForeignKey("BookIsbn")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__Authors_o__book___6383C8BA"),
                    l => l.HasOne<Author>().WithMany()
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__Authors_o__autho__628FA481"),
                    j =>
                    {
                        j.HasKey("AuthorId", "BookIsbn").HasName("PK__Authors___62D322507D82B4C8");
                        j.ToTable("Authors_of_books");
                        j.IndexerProperty<int>("AuthorId").HasColumnName("author_id");
                        j.IndexerProperty<int>("BookIsbn").HasColumnName("book_isbn");
                    });
        });

        modelBuilder.Entity<Book>(entity =>
        {
            entity.HasKey(e => e.BookIsbn).HasName("PK__Books__482499FF08EA90F6");

            entity.Property(e => e.BookIsbn).HasColumnName("book_isbn");
            entity.Property(e => e.BookCopyRight).HasColumnName("book_copy_right");
            entity.Property(e => e.BookEdition).HasColumnName("book_edition");
            entity.Property(e => e.BookTitle)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("book_title");
            entity.Property(e => e.Price)
                .HasColumnType("decimal(18, 0)")
                .HasColumnName("price");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
