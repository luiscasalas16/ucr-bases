global using DemoEfAdvanced.Database;
global using DemoEfAdvanced.Endpoints;
global using DemoEfAdvanced.Models;
global using DemoEfAdvanced.Utils;
global using Microsoft.AspNetCore.Mvc;
global using Microsoft.EntityFrameworkCore;

namespace DemoEfAdvanced;

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

        app.MapWriteEndpoints();
        app.MapReadUnmappedEndpoints();
        app.MapReadMappedEndpoints();

        app.Run();
    }
}
