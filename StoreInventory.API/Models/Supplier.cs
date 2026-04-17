using System.ComponentModel.DataAnnotations;

namespace StoreInventory.API.Models;

public class Supplier
{
    public int Id { get; set; }
    [Required]
    public string? Name { get; set; }
}