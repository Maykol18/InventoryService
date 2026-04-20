using StoreInventory.API.Models;

namespace StoreInventory.API.Services;

public interface IOrderService
{
    List<Order> GetAll();
    Order GetById(int id);
    void PlaceOrder(Order order);
    Order UpdateOrder(int id, Order order);
    void CancelOrder(int id);
}