

using LibraryManagementSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;

namespace LibraryManagementSystem.Infrastructure.Data;

public partial class LibraryDbContext : DbContext
{
    public LibraryDbContext()
    {
    }

    public LibraryDbContext(DbContextOptions<LibraryDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Author> Authors { get; set; }

    public virtual DbSet<Book> Books { get; set; }

    public virtual DbSet<BorrowingHistory> BorrowingHistories { get; set; }

    public virtual DbSet<LibraryBranch> LibraryBranches { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=DefaultConnection");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Author>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Author__3214EC07DDFC79B9");

            entity.ToTable("Author");

            entity.Property(e => e.FirstName)
                .HasMaxLength(25)
                .IsUnicode(false);
            entity.Property(e => e.LastName)
                .HasMaxLength(25)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Book>(entity =>
        {
            entity.HasKey(e => e.Isbn).HasName("PK__Book__9271CED16A751B96");

            entity.ToTable("Book");

            entity.Property(e => e.Price).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.Title)
                .HasMaxLength(150)
                .IsUnicode(false);

            entity.HasOne(d => d.Author).WithMany(p => p.Books)
                .HasForeignKey(d => d.AuthorId)
                .HasConstraintName("FK__Book__AuthorId__4E88ABD4");

            entity.HasOne(d => d.Branch).WithMany(p => p.Books)
                .HasForeignKey(d => d.BranchId)
                .HasConstraintName("FK__Book__BranchId__4F7CD00D");
            entity.Property(e => e.Status)
                  .HasColumnName("Status")
                  .HasConversion<int>();
        });

        modelBuilder.Entity<BorrowingHistory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Borrowin__3214EC075EB05C2C");

            entity.ToTable("BorrowingHistory");

            entity.HasOne(d => d.Book).WithMany(p => p.BorrowingHistories)
                .HasForeignKey(d => d.BookId)
                .HasConstraintName("FK__Borrowing__BookI__5441852A");

            entity.HasOne(d => d.User).WithMany(p => p.BorrowingHistories)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Borrowing__UserI__5535A963");
        });

        modelBuilder.Entity<LibraryBranch>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__LibraryB__3214EC0781E001CB");

            entity.ToTable("LibraryBranch");

            entity.Property(e => e.Address)
                .HasMaxLength(300)
                .IsUnicode(false);
            entity.Property(e => e.BranchName)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.ContactNumber)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__User__3214EC07EF460D35");

            entity.ToTable("User");

            entity.Property(e => e.Email)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.FirstName)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.LastName)
                .HasMaxLength(200)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
