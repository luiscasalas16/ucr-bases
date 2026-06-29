namespace DemoInjection.Endpoints;

internal static class InjectionEndpoints
{
    public static IEndpointRouteBuilder MapInjectionEndpoints(this IEndpointRouteBuilder builder)
    {
        var group = builder.MapGroup("/Injection").WithTags("Injection Examples");

        group.MapGet("/SearchEmpleados1", SearchEmpleados1);
        group.MapGet("/SearchEmpleados2", SearchEmpleados2);
        group.MapGet("/SearchEmpleados3", SearchEmpleados3);
        group.MapGet("/SearchEmpleados4", SearchEmpleados4);
        group.MapGet("/SearchEmpleados5", SearchEmpleados5);
        group.MapGet("/SearchEmpleados6", SearchEmpleados6);
        group.MapGet("/SearchEmpleados7", SearchEmpleados7);
        group.MapGet("/SearchEmpleados8", SearchEmpleados8);
        group.MapGet("/SearchEmpleados9", SearchEmpleados9);
        group.MapGet("/SearchEmpleados10", SearchEmpleados10);

        return builder;
    }

    static Task<List<Empleado>> SearchEmpleados1(
        string nombre,
        [FromServices] EmpresaContext context
    )
    {
        return context.Empleados.Where(c => c.Nombre.Contains(nombre)).ToListAsync();
    }

    static Task<List<Empleado>> SearchEmpleados2(
        string nombre,
        [FromServices] EmpresaContext context
    )
    {
        return context
            .Empleados.FromSql($"SELECT * FROM rh.Empleado WHERE Nombre LIKE {"%" + nombre + "%"};")
            .ToListAsync();
    }

    static Task<List<Empleado>> SearchEmpleados3(
        string nombre,
        [FromServices] EmpresaContext context
    )
    {
        return context
            .Empleados.FromSqlInterpolated(
                $"SELECT * FROM rh.Empleado WHERE Nombre LIKE {"%" + nombre + "%"};"
            )
            .ToListAsync();
    }

    static Task<List<Empleado>> SearchEmpleados4(
        string nombre,
        [FromServices] EmpresaContext context
    ) =>
        context
            .Empleados.FromSqlRaw($"SELECT * FROM rh.Empleado WHERE Nombre LIKE '%{nombre}%';")
            .ToListAsync();

    static Task<List<Empleado>> SearchEmpleados5(
        string nombre,
        [FromServices] EmpresaContext context
    )
    {
        return context
            .Empleados.FromSqlRaw(
                "SELECT * FROM rh.Empleado WHERE Nombre LIKE {0};",
                "%" + nombre + "%"
            )
            .ToListAsync();
    }

    static Task<List<Empleado>> SearchEmpleados6(
        string nombre,
        [FromServices] EmpresaContext context
    )
    {
        return context
            .Empleados.FromSqlRaw(
                $"SELECT * FROM rh.Empleado WHERE Nombre LIKE '%" + nombre + "%';"
            )
            .ToListAsync();
    }

    static Task<List<Empleado>> SearchEmpleados7(
        string nombre,
        [FromServices] EmpresaContext context
    )
    {
        return context.Empleados.FromSql($"EXEC rh.spSearchEmpleados7 {nombre}").ToListAsync();
    }

    static Task<List<Empleado>> SearchEmpleados8(
        string nombre,
        [FromServices] EmpresaContext context
    )
    {
        return context.Empleados.FromSql($"EXEC rh.spSearchEmpleados8 {nombre}").ToListAsync();
    }

    static Task<List<Empleado>> SearchEmpleados9(
        string nombre,
        [FromServices] EmpresaContext context
    )
    {
        return context.Empleados.FromSql($"EXEC rh.spSearchEmpleados9 {nombre}").ToListAsync();
    }

    static Task<List<Empleado>> SearchEmpleados10(
        string nombre,
        [FromServices] EmpresaContext context
    )
    {
        return context.Empleados.FromSql($"EXEC rh.spSearchEmpleados10 {nombre}").ToListAsync();
    }
}
