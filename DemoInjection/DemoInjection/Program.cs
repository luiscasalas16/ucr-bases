global using DemoInjection.Database;
global using DemoInjection.Endpoints;
global using DemoInjection.Models;
global using DemoInjection.Utils;
global using Microsoft.AspNetCore.Mvc;
global using Microsoft.EntityFrameworkCore;

namespace DemoInjection;

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

        app.MapInjectionEndpoints();

        app.Run();
    }
}
