using System.IO.Pipelines;
using StoreInventory.API.Models;
using StoreInventory.API.Services;

namespace StoreInventory.Tests;
public class ProductServiceTests
{
    private IProductService service = new ProductService();
    [Fact]
    public void Should_ReturnListOfProducts()
    {
        var products = service.GetAll();

        Assert.NotNull(products);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    public void Should_ReturnASpecificProduct_When_ProductFound(int id)
    {
        var product = service.GetById(id);

        Assert.NotNull(product);
    }

    [Theory]
    [InlineData(3000)]
    [InlineData(9000)]

    public void Should_ThrowApplicationException_When_ProductNotFound(int id)
    {
        var product = service.GetById(id);

        Assert.Null(product);        
    }

    [Fact]
    public void Should_AddProduct_When_CreatingANewProduct()
    {
        var product = new Product{ Name = "new product", Price = 12.5m, Stock = 3};
        var countBeforeAdd = service.GetAll().Count;
        service.AddProduct(product);
        var countAfterAdd = service.GetAll().Count;

        Assert.Equal(product, service.GetAll().FirstOrDefault(p => p.Name == product.Name));
        Assert.Equal(countBeforeAdd + 1, countAfterAdd);
    }

    [Theory]
    [InlineData(1, "random1", 25.6, 28)]
    [InlineData(2, "random2", 30.6, 38)]
    [InlineData(3, "random3", 35.6, 48)]
    public void Should_UpdateProduct_When_ProductFoundById(int id, string name, decimal price, int stock)
    {
        var productToUpdate = new Product{Name = name, Price = price, Stock = stock};
        service.UpdateProduct(id, productToUpdate);

        Assert.Equal(name, service.GetAll().FirstOrDefault(p => p.Id == id)!.Name);
        Assert.Equal(price, service.GetAll().FirstOrDefault(p => p.Id == id)!.Price);
        Assert.Equal(stock, service.GetAll().FirstOrDefault(p => p.Id == id)!.Stock);
    }

    [Theory]
    [InlineData(3000, "random1", 25.6, 28)]
    [InlineData(9000, "random2", 30.6, 38)]
    public void Should_ThrowApplicationException_When_ProductIsNotFoundById(int id, string name, decimal price, int stock)
    {           
        Assert.Throws<NullReferenceException>(() =>
        {
            var productToUpdate = new Product{Name = name, Price = price, Stock = stock};
            service.UpdateProduct(id, productToUpdate);
        });
    }

    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    public void Should_DeleteProduct_When_ProductFoundById(int id)
    {
        var product = service.GetById(id);
        service.DeleteProduct(id);

        Assert.NotNull(product);
        Assert.Null(service.GetAll().FirstOrDefault(p => p.Id == id));        
    }

    [Theory]
    [InlineData(3000)]
    [InlineData(9000)]
    public void Should_ThrowApplicationException_When_DeletingProductNotFound(int id)
    {
        var countBeforeAdd = service.GetAll().Count;
        service.DeleteProduct(id);
        var countAfterAdd = service.GetAll().Count;

        Assert.Equal(countBeforeAdd, countAfterAdd);
    }
}