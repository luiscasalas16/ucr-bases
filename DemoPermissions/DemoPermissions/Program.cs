global using DemoPermissions.Database;
global using DemoPermissions.Endpoints;
global using DemoPermissions.Models;
global using DemoPermissions.Utils;
global using Microsoft.AspNetCore.Mvc;
global using Microsoft.EntityFrameworkCore;

namespace DemoPermissions;

static class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddDbContext<EmpresaContext>();

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.MapPermissionsEndpoints();

        app.Run();
    }
}
