namespace DemoEfAdvanced.Endpoints;

internal static class WriteMappedEndpoints
{
    public static IEndpointRouteBuilder MapWriteMappedEndpoints(this IEndpointRouteBuilder builder)
    {
        var group = builder.MapGroup("/write").WithTags("Write Mapped Examples");

        group.MapGet("/InsertEmpleadoMapped", InsertEmpleadoMapped);

        return builder;
    }

    public static async Task InsertEmpleadoMapped([FromServices] EmpresaContextUsp context)
    {
        var empleado = Faker.GenerateEmpleadoUspFake();

        context.EmpleadosUsp.Add(empleado);

        await context.SaveChangesAsync();
    }
}
