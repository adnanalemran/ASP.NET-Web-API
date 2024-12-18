

var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

app.UseHttpsRedirection();

List<Category> categories = new List<Category>();

app.MapGet("/", () =>
{
    Console.WriteLine("Hit  Root Path");
    return Results.Json(new
    {
        message = "Api working fine ",
        date = DateTime.Now,
        name = "Test"
    });

});


//Read => Get: /api/categories 
app.MapGet("/api/categories", () =>
{
    return Results.Json(new
    {
        status = 200,
        data = categories,

    });

});
//Read => Get: /api/categories/{id}
app.MapGet("/api/categories/{id}", (Guid id) =>
{
    var category = categories.FirstOrDefault(x => x.CategoryId == id);
    if (category == null)
    {
        return Results.NotFound();
    }
    return Results.Json(new
    {
        status = 200,
        data = category,

    });
});


//Create => Post: /api/categories
app.MapPost("/api/categories", () =>
{
    var newCategory = new Category
    {

        CategoryId = Guid.NewGuid(),
        Name = "ELeoctonic 2",
        Description = "Test Dis 2 ",
        CreatedAt = DateTime.UtcNow,

    };
    categories.Add(newCategory);
    return Results.Created($"/api/categories/{newCategory.CategoryId}", newCategory);

});

//Delete => Delete: /api/categories/{id}
app.MapDelete("/api/categories/{id}", (Guid id) =>
{
    var category = categories.FirstOrDefault(x => x.CategoryId == id);
    if (category == null)
    {
        return Results.NotFound();
    }
    categories.Remove(category);
    return Results.NoContent();
});

//Update => Put: /api/categories/{id}
app.MapPut("/api/categories/{id}", (Guid id, Category category) =>
{
    var existingCategory = categories.FirstOrDefault(x => x.CategoryId == id);
    if (existingCategory == null)
    {
        return Results.NotFound();
    }
    existingCategory.Name = category.Name;
    existingCategory.Description = category.Description;

    return Results.Json(new
    {
        status = 200,
        data = existingCategory
    });
});

app.Run();

public record Category
{
    public Guid CategoryId { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public DateTime CreatedAt { get; set; }

};

//CRUD
//Create => Post: /api/categories
//Read => Get: /api/categories
//Read => Get: /api/categories/{id}
//Update => Put: /api/categories/{id}
//Delete => Delete: /api/categories/{id}
