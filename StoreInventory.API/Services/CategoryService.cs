using StoreInventory.API.Models;

namespace StoreInventory.API.Services;

public class CategoryService : ICategoryService
{
    List<Category> mockCategories = new List<Category>
    {
        new Category{Id = 1, Name = "Carnes"},
        new Category{Id = 2, Name = "Lacteos"},
        new Category{Id = 3, Name = "Embutidos"}
    };

    public void AddCategory(Category category)
    {
        category.Id = mockCategories.Count + 1;
        mockCategories.Add(category);
    }

    public void DeleteCategory(int id)
    {
        var category = mockCategories.FirstOrDefault(c => c.Id == id)!;
        mockCategories.Remove(category);
    }

    public List<Category> GetAll()
    {
        return mockCategories;
    }

    public Category GetById(int id)
    {
        var category = mockCategories.FirstOrDefault(c => c.Id == id)!;
        
        return category;
    }

    public Category UpdateCategory(int id, Category dto)
    {
        var category = mockCategories.FirstOrDefault(c => c.Id == id)!;
        category.Name = dto.Name;

        return category;
    }
}