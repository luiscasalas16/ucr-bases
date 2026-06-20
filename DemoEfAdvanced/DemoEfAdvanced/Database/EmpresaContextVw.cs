namespace DemoEfAdvanced.Database;

internal class EmpresaContextVw : DbContext
{
    private readonly string _connectionString;

    public EmpresaContextVw(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("EmpresaContext")!;
    }

    public EmpresaContextVw(
        DbContextOptions<EmpresaContextVw> options,
        IConfiguration configuration
    )
        : base(options)
    {
        _connectionString = configuration.GetConnectionString("EmpresaContext")!;
    }

    // Declare class to view map.
    public virtual DbSet<EmpleadoVw> EmpleadoVw { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
        optionsBuilder.UseSqlServer(_connectionString);

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configure class to view map.
        modelBuilder.Entity<EmpleadoVw>().HasNoKey().ToView("vwEmpleados", "rh");
    }
}
