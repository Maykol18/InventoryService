using StoreInventory.API.Models;
using StoreInventory.API.Services;

namespace StoreInventory.Tests;

public class CategoryServiceTests
{
    ICategoryService service = new CategoryService();

    [Fact]
    public void Should_ReturnListOfProducts()
    {
        var categories = service.GetAll();

        Assert.NotNull(categories);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    public void Should_ReturnASpecificCategory_When_CategoryFound(int id)
    {
        var category = service.GetById(id);

        Assert.NotNull(category);
    }

    [Theory]
    [InlineData(3000)]
    [InlineData(9000)]
    public void Should_ThrowApplicationException_When_CategoryIsNotFoundById(int id)
    {
        var category = service.GetById(id);

        Assert.Null(category);
    }

    [Fact]
    public void Should_AddCategory_When_CreatingANewCategory()
    {
        var category = new Category { Name = "Skin Care" };
        var countBeforeAdd = service.GetAll().Count;
        service.AddCategory(category);
        var countAfterAdd = service.GetAll().Count;

        Assert.Equal(category, service.GetAll().FirstOrDefault(c => c.Name == category.Name));
        Assert.Equal(countBeforeAdd + 1, countAfterAdd);
    }

    [Theory]
    [InlineData(1, "random1")]
    [InlineData(2, "random2")]
    [InlineData(3, "random3")]
    public void Should_UpdateCategory_When_CategoryFoundById(int id, string name)
    {
        var categoryToUpdate = new Category { Name = name };
        service.UpdateCategory(id, categoryToUpdate);

        var category = service.GetById(id);

        Assert.Equal(name, category.Name);
    }

    [Theory]
    [InlineData(3000, "random1")]
    [InlineData(9000, "random2")]
    public void Should_ThrowNullReferenceException_When_ProductIsNotFoundById(int id, string name)
    {
        Assert.Throws<NullReferenceException>(() =>
        {
            var category = new Category { Name = name };
            service.UpdateCategory(id, category);
        });
    }

    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    public void Should_DeleteCategory_When_CategoryFoundById(int id)
    {
        var category = service.GetById(id);
        service.DeleteCategory(id);

        Assert.NotNull(category);
        Assert.Null(service.GetAll().FirstOrDefault(c => c.Id == id));
    }

    [Theory]
    [InlineData(3000)]
    [InlineData(9000)]
    public void ShouldNot_DeleteCategory_When_CategoryNotFound(int id)
    {
        var countBeforeAdd = service.GetAll().Count;
        service.DeleteCategory(id);
        var countAfterAdd = service.GetAll().Count;

        Assert.Equal(countBeforeAdd, countAfterAdd);
    }
}