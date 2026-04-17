using StoreInventory.API.Models;

namespace StoreInventory.API.Services;

public interface ICategoryService
{
    List<Category> GetAll();
    Category GetById(int id);
    void AddCategory(Category category);
    Category UpdateCategory(int id, Category dto);
    void DeleteCategory(int id);
}