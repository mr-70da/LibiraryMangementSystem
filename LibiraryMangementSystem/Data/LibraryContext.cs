using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using LibraryManagementSystem.Models;

namespace LibraryManagementSystem.Data;

public partial class LibraryContext : DbContext
{
    public LibraryContext()
    {
    }

    public LibraryContext(DbContextOptions<LibraryContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Author> Authors { get; set; }

    public virtual DbSet<Book> Books { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.UseSqlServer("Name=DefaultConnection");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Author>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Author__3214EC07C6735B95");

            entity.ToTable("Author");

            entity.Property(e => e.FirstName)
                .HasMaxLength(25)
                .IsUnicode(false);
            entity.Property(e => e.LastName)
                .HasMaxLength(25)
                .IsUnicode(false);

            entity.HasMany(d => d.BookIsbns).WithMany(p => p.Authors)
                .UsingEntity<Dictionary<string, object>>(
                    "AuthorsOfBook",
                    r => r.HasOne<Book>().WithMany()
                        .HasForeignKey("BookIsbn")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__Authors_o__book___4F7CD00D"),
                    l => l.HasOne<Author>().WithMany()
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__Authors_o__autho__4E88ABD4"),
                    j =>
                    {
                        j.HasKey("AuthorId", "BookIsbn").HasName("PK__Authors___62D32250ACCF2237");
                        j.ToTable("Authors_of_books");
                        j.IndexerProperty<int>("AuthorId").HasColumnName("authorId");
                        j.IndexerProperty<int>("BookIsbn").HasColumnName("bookIsbn");
                    });
        });

        modelBuilder.Entity<Book>(entity =>
        {
            entity.HasKey(e => e.Isbn).HasName("PK__Book__9271CED1FA967165");
            entity.ToTable("Book");
            entity.Property(e => e.Price).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.Title)
                .HasMaxLength(150)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
