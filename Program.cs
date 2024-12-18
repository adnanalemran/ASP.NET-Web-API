using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.UseHttpsRedirection();

// In-memory storage for categories
List<Category> categories = new List<Category>();

// Root endpoint
app.MapGet("/", () =>
{
    Console.WriteLine("Hit Root Path");
    return Results.Json(new
    {
        message = "API working fine",
        date = DateTime.Now,
        name = "Test"
    });
});

// Get all categories with optional search
app.MapGet("/api/categories", ([FromQuery] string? searchValue = "") =>
{
    Console.WriteLine($"Search Value: {searchValue}");
    var result = categories.AsEnumerable();

    // Search by name, case-insensitive
    if (!string.IsNullOrEmpty(searchValue))
    {
        result = result.Where(c => !string.IsNullOrEmpty(c.Name) 
                                   && c.Name.Contains(searchValue, StringComparison.OrdinalIgnoreCase));
    }

    return Results.Ok(new
    {
        status = 200,
        data = result.ToList()
    });
});

// Get a specific category by ID
app.MapGet("/api/categories/{id}", (Guid id) =>
{
    var category = categories.FirstOrDefault(x => x.CategoryId == id);
    if (category == null)
    {
        return Results.NotFound(new { status = 404, message = "Category not found" });
    }
    return Results.Ok(new
    {
        status = 200,
        data = category
    });
});

// Create a new category
app.MapPost("/api/categories", ([FromBody] Category categoryData) =>
{
    if (string.IsNullOrWhiteSpace(categoryData.Name))
    {
        return Results.BadRequest(new { status = 400, message = "Category name is required" });
    }

    var newCategory = new Category
    {
        CategoryId = Guid.NewGuid(),
        Name = categoryData.Name,
        Description = categoryData.Description,
        CreatedAt = DateTime.Now
    };

    categories.Add(newCategory);

    return Results.Created($"/api/categories/{newCategory.CategoryId}", new
    {
        status = 201,
        data = newCategory
    });
});

// Update an existing category by ID
app.MapPut("/api/categories/{id}", (Guid id, [FromBody] Category categoryData) =>
{
    var existingCategory = categories.FirstOrDefault(x => x.CategoryId == id);
    if (existingCategory == null)
    {
        return Results.NotFound(new { status = 404, message = "Category not found" });
    }

    if (string.IsNullOrWhiteSpace(categoryData.Name))
    {
        return Results.BadRequest(new { status = 400, message = "Category name is required" });
    }

    existingCategory.Name = categoryData.Name;
    existingCategory.Description = categoryData.Description;

    return Results.Ok(new
    {
        status = 200,
        data = existingCategory
    });
});

// Delete a category by ID
app.MapDelete("/api/categories/{id}", (Guid id) =>
{
    var category = categories.FirstOrDefault(x => x.CategoryId == id);
    if (category == null)
    {
        return Results.NotFound(new { status = 404, message = "Category not found" });
    }

    categories.Remove(category);

    return Results.NoContent();
});

app.Run();

// Category model
public record Category
{
    public Guid CategoryId { get; set; } = Guid.NewGuid();
    public string? Name { get; set; }
    public string? Description { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
}
