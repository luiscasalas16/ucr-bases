using Microsoft.Data.SqlClient;

namespace DemoEfErrors.Endpoints;

internal static class ErrorsEndpoints
{
    public static IEndpointRouteBuilder MapErrorsEndpoints(this IEndpointRouteBuilder builder)
    {
        var group = builder.MapGroup("/errors").WithTags("Errors Examples");

        group.MapGet("/ErrorPk", ErrorPk);
        group.MapGet("/ErrorUnique", ErrorUnique);
        group.MapGet("/ErrorFk", ErrorFk);
        group.MapGet("/ErrorCheck", ErrorCheck);

        return builder;
    }

    static async Task<IResult> ErrorPk([FromServices] EmpresaContext context)
    {
        try
        {
            var empleadoFake = Faker.GenerateEmpleadoFake();

            empleadoFake.Cedula = (await context.Empleados.AsNoTracking().FirstAsync()).Cedula;

            context.Empleados.Add(empleadoFake);

            await context.SaveChangesAsync();
        }
        catch (DbUpdateException ex)
            when (ex.InnerException is SqlException sqlEx
                && (sqlEx.Number == 2601 || sqlEx.Number == 2627)
                && sqlEx.Message.Contains("PK_Empleado")
            )
        {
            return Results.BadRequest($"Error del PK del empleado.");
        }

        return Results.Ok();
    }

    static async Task<IResult> ErrorFk([FromServices] EmpresaContext context)
    {
        try
        {
            var empleadoFake = Faker.GenerateEmpleadoFake();

            empleadoFake.DepartamentoNumero = 0;

            context.Empleados.Add(empleadoFake);

            await context.SaveChangesAsync();
        }
        catch (DbUpdateException ex)
            when (ex.InnerException is SqlException sqlEx
                && sqlEx.Number == 547
                && sqlEx.Message.Contains("FK_Empleado_Departamento")
            )
        {
            return Results.BadRequest($"Error del FK del empleado con el departamento.");
        }

        return Results.Ok();
    }

    static async Task<IResult> ErrorUnique([FromServices] EmpresaContext context)
    {
        try
        {
            var empleadooFake = Faker.GenerateEmpleadoFake();

            empleadooFake.Correo = (await context.Empleados.AsNoTracking().FirstAsync()).Correo;

            context.Empleados.Add(empleadooFake);

            await context.SaveChangesAsync();
        }
        catch (DbUpdateException ex)
            when (ex.InnerException is SqlException sqlEx
                && (sqlEx.Number == 2601 || sqlEx.Number == 2627)
                && sqlEx.Message.Contains("UN_Empleado_Correo")
            )
        {
            return Results.BadRequest($"Error de único del correo del empleado.");
        }

        return Results.Ok();
    }

    static async Task<IResult> ErrorCheck([FromServices] EmpresaContext context)
    {
        try
        {
            var empleado = Faker.GenerateEmpleadoFake();

            empleado.Salario = -1;

            context.Empleados.Add(empleado);

            await context.SaveChangesAsync();
        }
        catch (DbUpdateException ex)
            when (ex.InnerException is SqlException sqlEx
                && sqlEx.Number == 547
                && sqlEx.Message.Contains("CH_Empleado_Salario")
            )
        {
            return Results.BadRequest($"Error del check de salario del empleado.");
        }

        return Results.Ok();
    }
}
