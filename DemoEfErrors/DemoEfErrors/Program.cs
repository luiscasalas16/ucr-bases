global using DemoEfErrors.Database;
global using DemoEfErrors.Endpoints;
global using DemoEfErrors.Models;
global using DemoEfErrors.Utils;
global using Microsoft.AspNetCore.Mvc;
global using Microsoft.EntityFrameworkCore;

namespace DemoEfErrors;

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

        app.MapErrorsEndpoints();

        app.Run();
    }
}
