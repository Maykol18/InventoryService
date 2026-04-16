using StoreInventory.API.Models;

namespace StoreInventory.API.Services;

public class ProductService : IProductService
{
    public List<Product> MockProducts = new List<Product>
    {
        new Product{Id = 1, Name = "Colgate Luminus White", Price = 25.6m, Stock = 25},
        new Product{Id = 2, Name = "Colgate Triple Accion", Price = 35.8m, Stock = 30},
        new Product{Id = 3, Name = "Colgate Clean Mint", Price = 15.3m, Stock = 10}
    };

    public void AddProduct(Product product)
    {
        product.Id = MockProducts.Count + 1;
        MockProducts.Add(product);
    }

    public void DeleteProduct(int id)
    {
        var product = MockProducts.FirstOrDefault(x => x.Id == id)!;
        if(product == null)
            throw  new ApplicationException("Product not found");
        MockProducts.Remove(product);
    }

    public List<Product> GetAll()
    {
        return MockProducts;
    }

    public Product GetById(int id)
    {
        
        if(MockProducts.FirstOrDefault(x => x.Id == id)! == null)
            throw  new ApplicationException("Product not found");
        var product = MockProducts.FirstOrDefault(x => x.Id == id)!;
        return MockProducts.FirstOrDefault(x => x.Id == id)!;
    }

    public Product UpdateProduct(int id, Product product)
    {
        
        if(MockProducts.FirstOrDefault(x => x.Id == id)! == null)
            throw  new ApplicationException("Product not found");
        var product1 = MockProducts.FirstOrDefault(x => x.Id == id)!;
        product1.Name = product.Name;
        product1.Price = product.Price;
        product1.Stock = product.Stock;
        
        return product1;
    }
}