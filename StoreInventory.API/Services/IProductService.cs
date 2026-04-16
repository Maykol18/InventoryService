using StoreInventory.API.Models;

namespace StoreInventory.API.Services;

public interface IProductService
{
    List<Product> GetAll();
    Product GetById(int id);
    void AddProduct(Product product);
    Product UpdateProduct(int id, Product product);
    void DeleteProduct(int id);
}