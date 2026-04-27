namespace DemoEfBasic.Endpoints;

internal static class ReadEndpoints
{
    public static IEndpointRouteBuilder MapReadEndpoints(this IEndpointRouteBuilder builder)
    {
        var group = builder.MapGroup("/read").WithTags("Read Examples");

        group.MapGet("/GetEmpleadosEfMethodSyntax", GetEmpleadosEfMethodSyntax);
        group.MapGet("/GetEmpleadosEfQuerySyntax", GetEmpleadosEfQuerySyntax);
        group.MapGet("/GetEmpleadosSql", GetEmpleadosSql);

        return builder;
    }

    public record EmpleadoDto(
        string Cedula,
        string Nombre,
        string Apellidos,
        string Correo,
        int Salario
    );

    static Task<List<EmpleadoDto>> GetEmpleadosEfMethodSyntax(
        int departamentoNumero,
        [FromServices] EmpresaContext context
    )
    {
        return context
            .Empleados.Where(e => e.DepartamentoNumero == departamentoNumero)
            .OrderBy(e => e.Nombre)
            .Select(e => new EmpleadoDto(e.Cedula, e.Nombre, e.Apellidos, e.Correo, e.Salario))
            .ToListAsync();
    }

    static Task<List<EmpleadoDto>> GetEmpleadosEfQuerySyntax(
        int departamentoNumero,
        [FromServices] EmpresaContext context
    )
    {
        return (
            from e in context.Empleados
            where e.DepartamentoNumero == departamentoNumero
            orderby e.Nombre
            select new EmpleadoDto(e.Cedula, e.Nombre, e.Apellidos, e.Correo, e.Salario)
        ).ToListAsync();
    }

    static Task<List<EmpleadoDto>> GetEmpleadosSql(
        int departamentoNumero,
        [FromServices] EmpresaContext context
    )
    {
        return context
            .Database.SqlQuery<EmpleadoDto>(
                @$"SELECT Cedula, Nombre, Apellidos, Correo, Salario
                    FROM rh.Empleado AS e
                    WHERE DepartamentoNumero = {departamentoNumero}"
            )
            .ToListAsync();
    }
}
