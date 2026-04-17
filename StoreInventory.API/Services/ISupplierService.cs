using StoreInventory.API.Models;

namespace StoreInventory.API.Services;

public interface ISupplierService
{
    List<Supplier> GetAll();
    Supplier GetById(int id);
    void CreateSupplier(Supplier dto);
    Supplier UpdateSupplier(int id, Supplier dto);
    void DeleteSupplier(int id);    
}