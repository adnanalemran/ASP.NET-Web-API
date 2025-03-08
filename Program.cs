using ASP.NET_Web_API.Controllers;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.UseHttpsRedirection();

app.MapGet("/", () =>
{
    Console.WriteLine("Hit root ");
    return Results.Json(new
    {
        message = "API working fine",
        date = DateTime.Now,
        name = "Test"
    });
});

//   category endpoints
CategoryController.RegisterCategoryEndpoints(app);

app.Run();