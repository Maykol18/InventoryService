using Microsoft.AspNetCore.Mvc;
using StoreInventory.API.Models;
using StoreInventory.API.Services;

namespace StoreInventory.API.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductController : ControllerBase
{
    private IProductService _service = new ProductService();

    [HttpGet]
    public IActionResult GetAll()
    {
        var products = _service.GetAll();
        return Ok(products);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var product = _service.GetById(id);
        if(product == null)
            return NotFound();
        return Ok(product); 
    }
    [HttpPost]
    public IActionResult AddProduct(Product product)
    {
        _service.AddProduct(product);

        return Created();
    }

    [HttpPut]
    public IActionResult UpdateProduct(int id, Product product)
    {
        var product1 = _service.UpdateProduct(id, product);
        return Ok(product1);
    }

    [HttpDelete]
    public IActionResult DeleteProduct(int id)
    {
        _service.DeleteProduct(id);
        return NoContent();
    }
}