namespace DemoEfAdvanced.Database;

internal partial class EmpresaContextFn : DbContext
{
    private readonly string _connectionString;

    public EmpresaContextFn(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("EmpresaContext")!;
    }

    public EmpresaContextFn(
        DbContextOptions<EmpresaContextFn> options,
        IConfiguration configuration
    )
        : base(options)
    {
        _connectionString = configuration.GetConnectionString("EmpresaContext")!;
    }

    // Declare class to function map.
    public IQueryable<EmpleadoFn> fnGetEmpleados(int departamentoNumero) =>
        FromExpression(() => fnGetEmpleados(departamentoNumero));

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
        optionsBuilder.UseSqlServer(_connectionString);

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configure class to function map.
        modelBuilder.Entity<EmpleadoFn>().HasNoKey();
        modelBuilder
            .HasDbFunction(() => fnGetEmpleados(default))
            .HasName("fnGetEmpleados")
            .HasSchema("rh");
    }
}
