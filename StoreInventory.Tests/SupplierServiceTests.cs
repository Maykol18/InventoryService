using StoreInventory.API.Models;
using StoreInventory.API.Services;

namespace StoreInventory.Tests;

public class SupplierServiceTests
{
    ISupplierService service = new SupplierService();

    [Fact]
    public void Should_ReturnAListOfSuppliers()
    {
        var suppliers = service.GetAll();

        Assert.NotNull(suppliers);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    public void Should_ReturnSpecificSupplier_When_SupplierFound(int id)
    {
        var supplier = service.GetById(id);

        Assert.NotNull(supplier);
    }

    [Theory]
    [InlineData(3000)]
    [InlineData(9000)]
    public void Should_ReturnNull_When_SupplierNotFound(int id)
    {
        var supplier = service.GetById(id);

        Assert.Null(supplier);
    }

    [Fact]
    public void Should_AddSupplier_When_CreatingANewSupplier()
    {
        // Given
        var dto = new Supplier{ Name = "Grupo Ramos"};
        // When
        var countBeforeAdd = service.GetAll().Count;
        service.CreateSupplier(dto);
        var countAfterAdd = service.GetAll().Count;
        // Then
        Assert.Equal(dto, service.GetAll().FirstOrDefault(s => s.Name == dto.Name));
        Assert.Equal(countBeforeAdd + 1, countAfterAdd);

    }

    [Theory]
    [InlineData(1, "random1")]
    [InlineData(2, "random2")]
    [InlineData(3, "random3")]
    public void Should_UpdateSupplier_When_SupplierFound(int id, string name)
    {
        var supplierToUpdate = new Supplier { Name = name};

        service.UpdateSupplier(id, supplierToUpdate);

        var supplier = service.GetById(id);

        Assert.Equal(name, supplier.Name);
    }

    [Theory]
    [InlineData(3000, "random1")]
    [InlineData(9000, "random2")]
    public void Should_ThrowNullReferenceException_When_SupplierNotFound(int id, string name)
    {
        Assert.Throws<NullReferenceException>(() =>
        {
            var supplierToUpdate = new Supplier { Name = name};
            service.UpdateSupplier(id, supplierToUpdate);
        });
    }

    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    public void Should_DeleteSupplier_When_SupplierFoundById(int id)
    {
        var supplier = service.GetById(id);
        service.DeleteSupplier(id);

        Assert.NotNull(supplier);
        Assert.Null(service.GetAll().FirstOrDefault(s => s.Id == id));
    }

    [Theory]
    [InlineData(3000)]
    [InlineData(9000)]
    public void ShouldNot_DeleteSupplier_When_SupplierNotFound(int id)
    {
        var countBeforeAdd = service.GetAll().Count;
        service.DeleteSupplier(id);
        var countAfterAdd = service.GetAll().Count;

        Assert.Equal(countBeforeAdd, countAfterAdd);
    }
}