using EfInheritanceTpH.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EfInheritanceTpH.Infraestructure;

class DemoDbContext : DbContext
{
    private readonly string _connectionString;

    public DemoDbContext(string connectionString)
    {
        _connectionString = connectionString;
    }

    // DbSets representing each table in the database
    public DbSet<Content> Contents { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // Configuring the connection string to the SQL Server database
        optionsBuilder
            .UseSqlServer(_connectionString)
            // allow logging SQL queries on debug and console output
            .UseLoggerFactory(LoggerFactory.Create(builder => builder.AddDebug()));
    }

    // Configures the model and mappings between entities and database
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("tph");

        // Configuring Table-Per-Hierarchy (TPH) inheritance for Payment entities
        modelBuilder
            .Entity<Content>()
            .HasDiscriminator<ContentType>("ContentType") // Adds a discriminator column named 'ContentType'
            .HasValue<Article>(ContentType.Article) // Sets discriminator value 'Article' for Article entities
            .HasValue<Video>(ContentType.Video) // Sets discriminator value 'Video' for Video entities
            .HasValue<Image>(ContentType.Image); // Sets discriminator value 'Image' for Image entities

        // Configure enums to be stored as strings

        // For ContentStatus enum
        modelBuilder
            .Entity<Content>()
            .Property(c => c.Status)
            .HasConversion<string>()
            .IsRequired(); // Optional: Specify if the property is required
    }
}
