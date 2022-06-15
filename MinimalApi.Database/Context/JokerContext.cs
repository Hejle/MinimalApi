using Microsoft.EntityFrameworkCore;
using MinimalApi.Common.Models;
using MinimalApi.Database.Converters;

namespace MinimalApi.Database.Context;

public class JokerContext : DbContext
{
    public DbSet<Joker> Jokers { get; set; }

    public JokerContext(DbContextOptions<JokerContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Joker>(builder =>
        {
            // Date is a DateOnly property and date on database
            builder.Property(x => x.CreatedDate)
                .HasConversion<DateOnlyConverter, DateOnlyComparer>();
            builder.Property(x => x.JokeDay)
                .HasConversion<DateOnlyConverter, DateOnlyComparer>();
        });
        modelBuilder.Entity<Joker>().HasKey(x => new { x.JokerName, x.CreatedDate });
    }
}