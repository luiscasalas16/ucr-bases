namespace DemoEfAdvanced.Endpoints;

internal static class ReadMappedEndpoints
{
    public static IEndpointRouteBuilder MapReadMappedEndpoints(this IEndpointRouteBuilder builder)
    {
        var group = builder.MapGroup("/read").WithTags("Read Examples");

        group.MapGet("/GetEmpleadosSpMapped", GetEmpleadosSpMapped);
        group.MapGet("/GetEmpleadosFnMapped", GetEmpleadosFnMapped);
        group.MapGet("/GetEmpleadosVwMapped", GetEmpleadosVwMapped);

        return builder;
    }

    static Task<List<Empleado>> GetEmpleadosSpMapped(
        int departamentoNumero,
        [FromServices] EmpresaContext context
    )
    {
        return context
            .Empleados.FromSql($"EXEC rh.spGetEmpleados {departamentoNumero}")
            .ToListAsync();
    }

    static Task<List<EmpleadoFn>> GetEmpleadosFnMapped(
        int departamentoNumero,
        [FromServices] EmpresaContext context
    )
    {
        return context.fnGetEmpleados(departamentoNumero).ToListAsync();
    }

    static Task<List<EmpleadoVw>> GetEmpleadosVwMapped(
        int departamentoNumero,
        [FromServices] EmpresaContext context
    )
    {
        return context
            .EmpleadoVw.Where(e => e.DepartamentoNumero == departamentoNumero)
            .ToListAsync();
    }
}
