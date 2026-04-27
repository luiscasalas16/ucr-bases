using EfInheritanceTpC.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EfInheritanceTpC.Infraestructure;

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
        modelBuilder.HasDefaultSchema("tpc");

        // Configure the base class as abstract to prevent EF Core from creating a separate table
        modelBuilder.Entity<Content>().UseTpcMappingStrategy();

        // Configure TPC inheritance by mapping each concrete class to its own table
        modelBuilder.Entity<Article>(); // Table for Articles
        modelBuilder.Entity<Video>(); // Table for Videos
        modelBuilder.Entity<Image>(); // Table for Images

        // Configure enums to be stored as strings

        // For ContentStatus enum
        modelBuilder
            .Entity<Content>()
            .Property(c => c.Status)
            .HasConversion<string>()
            .IsRequired(); // Optional: Specify if the property is required
    }
}
