var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

app.UseHttpsRedirection();

app.MapGet("/", () =>
{
    Console.WriteLine("Hit it test ");
    return Results.Json(new
    {
        message = "Api working fine ",
        date = DateTime.Now,
        name = "Test"
    });

});


app.MapGet("hello", () =>
{
    Console.WriteLine("Hit it Hello ");
    return "Test";
});


app.Run();
