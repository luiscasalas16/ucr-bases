namespace DemoEfAdvanced.Endpoints;

internal static class WriteEndpoints
{
    public static IEndpointRouteBuilder MapWriteEndpoints(this IEndpointRouteBuilder builder)
    {
        var group = builder.MapGroup("/write").WithTags("Write Examples");

        group.MapGet("/InsertEmpleadoSp1", InsertEmpleadoSp1);
        group.MapGet("/InsertEmpleadoSp2", InsertEmpleadoSp2);

        return builder;
    }

    public static async Task InsertEmpleadoSp1([FromServices] EmpresaContext context)
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

        await context.SaveChangesAsync();
    }

    public static async Task InsertEmpleadoSp2([FromServices] EmpresaContext context)
    {
        var empleadoFake = Faker.GenerateEmpleadoFake();

        await context.Database.ExecuteSqlAsync(
            @$"EXEC rh.spInsertEmpleado 
                    {empleadoFake.Cedula}, 
                    {empleadoFake.Nombre},
                    {empleadoFake.Apellidos},
                    {empleadoFake.FechaNacimiento}, 
                    {empleadoFake.Direccion},
                    {empleadoFake.Correo},
                    {empleadoFake.Salario},
                    {empleadoFake.DepartamentoNumero},
                    {empleadoFake.SupervidorCedula}"
        );

        await context.SaveChangesAsync();
    }
}
