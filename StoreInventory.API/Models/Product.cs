using System.ComponentModel.DataAnnotations;

namespace StoreInventory.API.Models;
public class Product
{
    public int Id { get; set; }
    [Required]
    public string? Name { get; set; }
    public decimal Price { get; set; }
    public int Stock { get; set; }
}