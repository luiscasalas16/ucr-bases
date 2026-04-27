using EfInheritanceTpT.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EfInheritanceTpT.Infraestructure;

class DemoDbContext : DbContext
{
    private readonly string _connectionString;

    public DemoDbContext(string connectionString)
    {
        _connectionString = connectionString;
    }

    // DbSets representing each table in the database
    public DbSet<Content> Contents { get; set; }
    public DbSet<Article> Articles { get; set; }
    public DbSet<Video> Videos { get; set; }
    public DbSet<Image> Images { get; set; }

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
        // Configure the default schema for all tables
        modelBuilder.HasDefaultSchema("tpt");

        // Map each class in the hierarchy to its own table
        modelBuilder.Entity<Content>().ToTable("Contents"); // Base table for common properties
        modelBuilder.Entity<Article>().ToTable("Articles"); // Table for Articles
        modelBuilder.Entity<Video>().ToTable("Videos"); // Table for Videos
        modelBuilder.Entity<Image>().ToTable("Images"); // Table for Images

        // Configure enums to be stored as strings

        // For ContentType enum
        modelBuilder
            .Entity<Content>()
            .Property(c => c.ContentType)
            .HasConversion<string>()
            .IsRequired(); // Optional: Specify if the property is required

        // For ContentStatus enum
        modelBuilder
            .Entity<Content>()
            .Property(c => c.Status)
            .HasConversion<string>()
            .IsRequired(); // Optional: Specify if the property is required
    }
}
