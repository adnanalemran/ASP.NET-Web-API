namespace ASP.NET_Web_API.Services;

using System;
using System.Collections.Generic;
using System.Linq;
using ASP.NET_Web_API.Models;

public class CategoryService
{
    private readonly List<Category> _categories = new();

    public IEnumerable<Category> GetAllCategories(string? searchValue)
    {
        if (string.IsNullOrEmpty(searchValue))
            return _categories;

        return _categories.Where(c => !string.IsNullOrEmpty(c.Name) &&
                                      c.Name.Contains(searchValue, StringComparison.OrdinalIgnoreCase));
    }

    public Category? GetCategoryById(Guid id) => _categories.FirstOrDefault(x => x.CategoryId == id);

    public Category AddCategory(Category category)
    {
        var newCategory = new Category
        {
            CategoryId = Guid.NewGuid(),
            Name = category.Name,
            Description = category.Description,
            CreatedAt = DateTime.Now
        };
        _categories.Add(newCategory);
        return newCategory;
    }

    public Category? UpdateCategory(Guid id, Category categoryData)
    {
        var existingCategory = _categories.FirstOrDefault(x => x.CategoryId == id);
        
        if (existingCategory == null) return null;



        if (!string.IsNullOrWhiteSpace(categoryData.Name))
            existingCategory.Name = categoryData.Name;

        if (!string.IsNullOrWhiteSpace(categoryData.Description))
            existingCategory.Description = categoryData.Description;

        return existingCategory;
    }

    public bool DeleteCategory(Guid id)
    {
        var category = _categories.FirstOrDefault(x => x.CategoryId == id);
        if (category == null) return false;

        _categories.Remove(category);
        return true;
    }
}