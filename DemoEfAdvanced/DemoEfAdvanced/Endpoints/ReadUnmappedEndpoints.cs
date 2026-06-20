namespace DemoEfAdvanced.Endpoints;

internal static class ReadUnmappedEndpoints
{
    public static IEndpointRouteBuilder MapReadUnmappedEndpoints(this IEndpointRouteBuilder builder)
    {
        var group = builder.MapGroup("/read").WithTags("Read Unmapped Examples");

        group.MapGet("/GetEmpleadosReporteSpUnmapped", GetEmpleadosReporteSpUnmapped);
        group.MapGet("/GetEmpleadosReporteFnUnmapped", GetEmpleadosReporteFnUnmapped);
        group.MapGet("/GetEmpleadosReporteVwUnmapped", GetEmpleadosReporteVwUnmapped);

        return builder;
    }

    public record EmpleadoReporteDto(
        string Cedula,
        string Nombre,
        string Apellidos,
        int CantidadTelefonos,
        int CantidadFamiliares,
        int CantidadProyectos
    );

    static Task<List<EmpleadoReporteDto>> GetEmpleadosReporteSpUnmapped(
        int departamentoNumero,
        [FromServices] EmpresaContext context
    )
    {
        return context
            .Database.SqlQuery<EmpleadoReporteDto>(
                $"EXEC rh.spGetEmpleadosReporte {departamentoNumero}"
            )
            .ToListAsync();
    }

    static Task<List<EmpleadoReporteDto>> GetEmpleadosReporteFnUnmapped(
        int departamentoNumero,
        [FromServices] EmpresaContext context
    )
    {
        // ██╗    ██╗ █████╗ ██████╗ ███╗   ██╗██╗███╗   ██╗ ██████╗
        // ██║    ██║██╔══██╗██╔══██╗████╗  ██║██║████╗  ██║██╔════╝
        // ██║ █╗ ██║███████║██████╔╝██╔██╗ ██║██║██╔██╗ ██║██║  ███╗
        // ██║███╗██║██╔══██║██╔══██╗██║╚██╗██║██║██║╚██╗██║██║   ██║
        // ╚███╔███╔╝██║  ██║██║  ██║██║ ╚████║██║██║ ╚████║╚██████╔╝
        //  ╚══╝╚══╝ ╚═╝  ╚═╝╚═╝  ╚═╝╚═╝  ╚═══╝╚═╝╚═╝  ╚═══╝ ╚═════╝
        //
        // WARNING: Do not embed SQL directly in C# code.
        // This mixes responsibilities and makes the system harder to maintain and evolve.
        // Additionally, interpolating values into the query introduces security risks such
        // as SQL injection by bypassing proper parameterization.
        return context
            .Database.SqlQuery<EmpleadoReporteDto>(
                @$"SELECT Cedula, Nombre, Apellidos, DepartamentoNumero, CantidadTelefonos, CantidadFamiliares, CantidadProyectos 
                    FROM rh.fnGetEmpleadosReporte({departamentoNumero})"
            )
            .ToListAsync();
    }

    static Task<List<EmpleadoReporteDto>> GetEmpleadosReporteVwUnmapped(
        int departamentoNumero,
        [FromServices] EmpresaContext context
    )
    {
        // ██╗    ██╗ █████╗ ██████╗ ███╗   ██╗██╗███╗   ██╗ ██████╗
        // ██║    ██║██╔══██╗██╔══██╗████╗  ██║██║████╗  ██║██╔════╝
        // ██║ █╗ ██║███████║██████╔╝██╔██╗ ██║██║██╔██╗ ██║██║  ███╗
        // ██║███╗██║██╔══██║██╔══██╗██║╚██╗██║██║██║╚██╗██║██║   ██║
        // ╚███╔███╔╝██║  ██║██║  ██║██║ ╚████║██║██║ ╚████║╚██████╔╝
        //  ╚══╝╚══╝ ╚═╝  ╚═╝╚═╝  ╚═╝╚═╝  ╚═══╝╚═╝╚═╝  ╚═══╝ ╚═════╝
        //
        // WARNING: Do not embed SQL directly in C# code.
        // This mixes responsibilities and makes the system harder to maintain and evolve.
        // Additionally, interpolating values into the query introduces security risks such
        // as SQL injection by bypassing proper parameterization.
        return context
            .Database.SqlQuery<EmpleadoReporteDto>(
                @$"SELECT Cedula, Nombre, Apellidos, DepartamentoNumero, CantidadTelefonos, CantidadFamiliares, CantidadProyectos 
                    FROM rh.vwEmpleadosReporte
                    where DepartamentoNumero = {departamentoNumero}"
            )
            .ToListAsync();
    }
}
