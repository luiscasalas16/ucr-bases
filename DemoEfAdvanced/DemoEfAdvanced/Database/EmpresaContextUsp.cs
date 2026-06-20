namespace DemoEfAdvanced.Database;

internal partial class EmpresaContextUsp : DbContext
{
    private readonly string _connectionString;

    public EmpresaContextUsp(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("EmpresaContext")!;
    }

    public EmpresaContextUsp(
        DbContextOptions<EmpresaContextUsp> options,
        IConfiguration configuration
    )
        : base(options)
    {
        _connectionString = configuration.GetConnectionString("EmpresaContext")!;
    }

    // Declare class to stored procedure map.
    public virtual DbSet<EmpleadoUsp> EmpleadosUsp { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
        optionsBuilder.UseSqlServer(_connectionString);

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configure class to stored procedure map.
        modelBuilder.Entity<EmpleadoUsp>(entity =>
        {
            entity.HasKey(e => e.Cedula);

            entity.ToTable("Empleado", "rh");

            entity.InsertUsingStoredProcedure(
                "spInsertEmpleado",
                "rh",
                sp =>
                {
                    sp.HasParameter(e => e.Cedula);
                    sp.HasParameter(e => e.Nombre);
                    sp.HasParameter(e => e.Apellidos);
                    sp.HasParameter(e => e.FechaNacimiento);
                    sp.HasParameter(e => e.Direccion);
                    sp.HasParameter(e => e.Correo);
                    sp.HasParameter(e => e.Salario);
                    sp.HasParameter(e => e.DepartamentoNumero);
                    sp.HasParameter(e => e.SupervidorCedula);
                }
            );

            entity.UpdateUsingStoredProcedure(
                "spUpdateEmpleado",
                "rh",
                sp =>
                {
                    sp.HasOriginalValueParameter(e => e.Cedula);
                    sp.HasParameter(e => e.Nombre);
                    sp.HasParameter(e => e.Apellidos);
                    sp.HasParameter(e => e.FechaNacimiento);
                    sp.HasParameter(e => e.Direccion);
                    sp.HasParameter(e => e.Correo);
                    sp.HasParameter(e => e.Salario);
                    sp.HasParameter(e => e.DepartamentoNumero);
                    sp.HasParameter(e => e.SupervidorCedula);
                }
            );

            entity.DeleteUsingStoredProcedure(
                "spDeleteEmpleado",
                "rh",
                sp =>
                {
                    sp.HasOriginalValueParameter(e => e.Cedula);
                }
            );

            entity.HasIndex(e => e.Correo, "UN_Empleado_Correo").IsUnique();

            entity.Property(e => e.Cedula).HasMaxLength(10).IsUnicode(false);
            entity.Property(e => e.Apellidos).HasMaxLength(100).IsUnicode(false);
            entity.Property(e => e.Correo).HasMaxLength(100).IsUnicode(false);
            entity.Property(e => e.Direccion).HasMaxLength(200).IsUnicode(false);
            entity.Property(e => e.FechaNacimiento).HasColumnType("datetime");
            entity.Property(e => e.Nombre).HasMaxLength(100).IsUnicode(false);
            entity.Property(e => e.SupervidorCedula).HasMaxLength(10).IsUnicode(false);
        });
    }
}
