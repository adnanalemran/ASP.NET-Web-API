using ASP.NET_Web_API.Controllers;
using Microsoft.AspNetCore.Builder;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.UseHttpsRedirection();

app.MapGet("/", () => Results.Json(new
{
    message = "API working fine",
    date = DateTime.Now,
    name = "Test"
}));

// Register category endpoints
CategoryController.RegisterCategoryEndpoints(app);

app.Run();