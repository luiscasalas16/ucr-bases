namespace DemoEfAdvanced.Endpoints;

internal static class WriteUnmappedEndpoints
{
    public static IEndpointRouteBuilder MapWriteUnmappedEndpoints(
        this IEndpointRouteBuilder builder
    )
    {
        var group = builder.MapGroup("/write").WithTags("Write Unmapped Examples");

        group.MapGet("/InsertEmpleadoSp1Unmapped", InsertEmpleadoSp1Unmapped);
        group.MapGet("/InsertEmpleadoSp2Unmapped", InsertEmpleadoSp2Unmapped);

        return builder;
    }

    public static async Task InsertEmpleadoSp1Unmapped([FromServices] EmpresaContext context)
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

    public static async Task InsertEmpleadoSp2Unmapped([FromServices] EmpresaContext context)
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
    }
}
