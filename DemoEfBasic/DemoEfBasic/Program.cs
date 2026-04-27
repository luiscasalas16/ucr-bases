namespace DemoEfBasic
{
    static class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.MapGet(
                    "/weatherforecast",
                    (HttpContext httpContext) =>
                    {
                        return "hello world";
                    }
                )
                .WithName("GetWeatherForecast");

            app.Run();
        }
    }
}
