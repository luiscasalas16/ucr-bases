namespace DemoPermissions.Endpoints;

internal static class PermissionsEndpoints
{
    public static IEndpointRouteBuilder MapPermissionsEndpoints(this IEndpointRouteBuilder builder)
    {
        var readgroup = builder.MapGroup("/read").WithTags("Read Permissions Examples");

        readgroup.MapGet("/GetEmpleadosEf", GetEmpleadosEf);
        readgroup.MapGet("/GetEmpleadosSql", GetEmpleadosSql);
        readgroup.MapGet("/GetEmpleadosUsp", GetEmpleadosUsp);

        var writegroup = builder.MapGroup("/write").WithTags("Write Permissions Examples");

        writegroup.MapGet("/InsertEmpleadoEf", InsertEmpleadoEf);
        writegroup.MapGet("/InsertEmpleadoSql", InsertEmpleadoSql);
        writegroup.MapGet("/InsertEmpleadoUsp", InsertEmpleadoUsp);

        var databasegroup = builder.MapGroup("/database").WithTags("Database Permissions Examples");

        databasegroup.MapGet("/ChangeDatabaseQuery", ChangeDatabaseQuery);

        return builder;
    }

    static Task<List<EmpleadoDto>> GetEmpleadosEf([FromServices] EmpresaContext context)
    {
        return context
            .Empleados.Where(e => e.DepartamentoNumero == 1)
            .OrderBy(e => e.Nombre)
            .Select(e => new EmpleadoDto(e.Cedula, e.Nombre, e.Apellidos, e.Correo, e.Salario))
            .ToListAsync();
    }

    static Task<List<EmpleadoDto>> GetEmpleadosSql([FromServices] EmpresaContext context)
    {
        // WARNING: Do not embed SQL directly in C# code.
        return context
            .Database.SqlQuery<EmpleadoDto>(
                @$"SELECT Cedula, Nombre, Apellidos, Correo, Salario
                    FROM rh.Empleado AS e
                    WHERE DepartamentoNumero = 1"
            )
            .ToListAsync();
    }

    static Task<List<EmpleadoDto>> GetEmpleadosUsp([FromServices] EmpresaContext context)
    {
        return context.Database.SqlQuery<EmpleadoDto>($"EXEC rh.spGetEmpleados 1").ToListAsync();
    }

    static async Task InsertEmpleadoEf([FromServices] EmpresaContext context)
    {
        var empleadoFake = Faker.GenerateEmpleadoFake();

        context.Empleados.Add(empleadoFake);

        await context.SaveChangesAsync();
    }

    static async Task InsertEmpleadoSql([FromServices] EmpresaContext context)
    {
        var empleadoFake = Faker.GenerateEmpleadoFake();

        // WARNING: Do not embed SQL directly in C# code.
        await context.Database.ExecuteSqlAsync(
            @$"INSERT INTO rh.Empleado (Cedula, Nombre, Apellidos, FechaNacimiento, Direccion,
                          Correo, Salario, DepartamentoNumero, SupervidorCedula)
                   VALUES ({empleadoFake.Cedula}, {empleadoFake.Nombre}, {empleadoFake.Apellidos}, {empleadoFake.FechaNacimiento}, {empleadoFake.Direccion},
                          {empleadoFake.Correo}, {empleadoFake.Salario}, {empleadoFake.DepartamentoNumero}, {empleadoFake.SupervidorCedula});"
        );

        await context.SaveChangesAsync();
    }

    static async Task InsertEmpleadoUsp([FromServices] EmpresaContext context)
    {
        var empleadoFake = Faker.GenerateEmpleadoFake();

        await context.Database.ExecuteSqlAsync(
            @$"EXEC rh.spInsertEmpleado 
                    @Cedula={empleadoFake.Cedula}, 
                    @Nombre = {empleadoFake.Nombre},
                    @Apellidos = {empleadoFake.Apellidos},
                    @FechaNacimiento = {empleadoFake.FechaNacimiento}, 
                    @Direccion = {empleadoFake.Direccion},
                    @Correo = {empleadoFake.Correo},
                    @Salario = {empleadoFake.Salario},
                    @DepartamentoNumero = {empleadoFake.DepartamentoNumero},
                    @SupervidorCedula = {empleadoFake.SupervidorCedula}"
        );
    }

    static async Task ChangeDatabaseQuery([FromServices] EmpresaContext context)
    {
        await context.Database.ExecuteSqlAsync(
            @$"DROP TABLE IF EXISTS rh.Pueba;

                CREATE TABLE rh.Pueba 
                (
                    Numero INT,
                    Nombre VARCHAR(100) NOT NULL
                );"
        );

        await context.SaveChangesAsync();
    }
}
