namespace ASP.NET_Web_API.Controllers;

using Microsoft.AspNetCore.Mvc;
using ASP.NET_Web_API.Models;
using ASP.NET_Web_API.Services;

public static class CategoryController
{
    public static void RegisterCategoryEndpoints(WebApplication app)
    {
        var service = new CategoryService();

        app.MapGet("/api/categories", ([FromQuery] string? searchValue) =>
        {
            var result = service.GetAllCategories(searchValue);
            return Results.Ok(new { status = 200, data = result });
        });

        app.MapGet("/api/categories/{id:guid}", (Guid id) =>
        {
            var category = service.GetCategoryById(id);
            return category is not null
                ? Results.Ok(new { status = 200, data = category })
                : Results.NotFound(new { status = 404, message = "Category not found" });
        });

        app.MapPost("/api/categories", ([FromBody] Category categoryData) =>
        {
            if (string.IsNullOrWhiteSpace(categoryData.Name))
                return Results.BadRequest(new { status = 400, message = "Category name is required" });

            var newCategory = service.AddCategory(categoryData);
            return Results.Created($"/api/categories/{newCategory.CategoryId}", new { status = 201, data = newCategory });
        });

        app.MapPut("/api/categories/{id:guid}", (Guid id, [FromBody] Category categoryData) =>
        {
            if (service.UpdateCategory(id, categoryData))
                return Results.Ok(new { status = 200, message = "Category updated successfully" });

            return Results.NotFound(new { status = 404, message = "Category not found" });
        });

        app.MapDelete("/api/categories/{id:guid}", (Guid id) =>
        {
            if (service.DeleteCategory(id))
                return Results.Ok(new { status = 200, message = "Category deleted successfully" });

            return Results.NotFound(new { status = 404, message = "Category not found" });
        });
    }
}
