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
        if (!ModelState.IsValid)
        {
            return BadRequest(new
            {
                Message = "Datos invalidos",
                Errors = ModelState
            });
        }

        _service.AddProduct(product);

        return Created();
    }

    [HttpPut]
    public IActionResult UpdateProduct(int id, Product dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(new
            {
                Message = "Datos invalidos",
                Errors = ModelState
            });
        }

        var lookingForProduct = _service.GetById(id);
        if(lookingForProduct == null)
            return NotFound();

        var product = _service.UpdateProduct(id, dto);

        return Ok(product);
    }

    [HttpDelete]
    public IActionResult DeleteProduct(int id)
    {        
        var lookingForProduct = _service.GetById(id);
        if(lookingForProduct == null)
            return NotFound();

        _service.DeleteProduct(id);

        return NoContent();
    }
}