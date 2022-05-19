using Microsoft.EntityFrameworkCore;
using MinimalApi.Common.Models;

namespace MinimalApi.Database.Context;

public class BookContext : DbContext
{
    public DbSet<Book> Books { get; set; }

    public BookContext(DbContextOptions<BookContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .Entity<Book>()
            .Property(book => book.Isbn)
            .IsUnicode(false)
            .HasMaxLength(20);
        modelBuilder
            .Entity<Book>()
            .HasKey(book => book.Isbn);
    }
}