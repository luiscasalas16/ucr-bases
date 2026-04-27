namespace DemoEfBasic.Endpoints;

internal static class WriteEndpoints
{
    public static IEndpointRouteBuilder MapWriteEndpoints(this IEndpointRouteBuilder builder)
    {
        var group = builder.MapGroup("/write").WithTags("Write Examples");

        group.MapGet("/InsertEmpleadoEf", InsertEmpleadoEf);
        group.MapGet("/InsertEmpleadoSql", InsertEmpleadoSql);

        return builder;
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

        await context.Database.ExecuteSqlAsync(
            @$"INSERT INTO rh.Empleado (Cedula, Nombre, Apellidos, FechaNacimiento, Direccion,
                          Correo, Salario, DepartamentoNumero, SupervidorCedula)
                   VALUES ({empleadoFake.Cedula}, {empleadoFake.Nombre}, {empleadoFake.Apellidos}, {empleadoFake.FechaNacimiento}, {empleadoFake.Direccion},
                          {empleadoFake.Correo}, {empleadoFake.Salario}, {empleadoFake.DepartamentoNumero}, {empleadoFake.SupervidorCedula});"
        );

        await context.SaveChangesAsync();
    }
}
