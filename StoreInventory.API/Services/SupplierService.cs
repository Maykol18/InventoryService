using StoreInventory.API.Models;

namespace StoreInventory.API.Services;

public class SupplierService : ISupplierService
{
    List<Supplier> mockSuppliers = new List<Supplier>
    {
        new Supplier{ Id = 1, Name = "Amazon" },
        new Supplier{ Id = 2, Name = "Temu" },
        new Supplier{ Id = 3, Name = "Ebay" }
    };
    public void CreateSupplier(Supplier supplier)
    {
        supplier.Id = mockSuppliers.Count + 1;
        mockSuppliers.Add(supplier);
    }

    public void DeleteSupplier(int id)
    {
        var supplier = mockSuppliers.FirstOrDefault(s => s.Id == id)!;
        mockSuppliers.Remove(supplier);
    }

    public List<Supplier> GetAll()
    {
        var suppliers = mockSuppliers;

        return suppliers;
    }

    public Supplier GetById(int id)
    {
        var supplier = mockSuppliers.FirstOrDefault(s => s.Id == id)!;

        return supplier;
    }

    public Supplier UpdateSupplier(int id, Supplier dto)
    {
        var supplier = mockSuppliers.FirstOrDefault(s => s.Id == id)!;
        supplier.Name = dto.Name;

        return supplier;
    }
}