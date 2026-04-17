using System.ComponentModel.DataAnnotations;

namespace StoreInventory.API.Models;

public class Category
{
    public int Id { get; set; }
    [Required]
    public string? Name { get; set; }
}