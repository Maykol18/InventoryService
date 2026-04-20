using System.ComponentModel.DataAnnotations;

namespace StoreInventory.API.Models;

public class Order
{
    public int Id { get; set; }
    [Required]
    public List<OrderItem> Products { get; set; } = new List<OrderItem>();
    public decimal TotalAmount { get; set; }
    public DateTime Date { get; set; }
}