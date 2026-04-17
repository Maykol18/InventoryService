using Microsoft.AspNetCore.Mvc;
using StoreInventory.API.Models;
using StoreInventory.API.Services;

namespace StoreInventory.API.Controllers;

[ApiController]
[Route("[controller]")]
public class CategoryController : ControllerBase
{
    ICategoryService service = new CategoryService();

    [HttpGet]
    public IActionResult GetAll()
    {
        var categories = service.GetAll();

        return Ok(categories);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var category = service.GetById(id);
        if (category == null)
            return NotFound();

        return Ok(category);
    }

    [HttpPost]
    public IActionResult AddCategory(Category category)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(new
            {
                Message = "Datos invalidos",
                Errors = ModelState
            });
        }

        service.AddCategory(category);

        return Created();
    }

    [HttpPut]
    public IActionResult UpdateCategory(int id, Category dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(new
            {
                Message = "Datos invalidos",
                Errors = ModelState
            });
        }

        var lookingForCategory = service.GetById(id);
        if (lookingForCategory == null)
            return NotFound();

        var category = service.UpdateCategory(id, dto);
        
        return Ok(category);
    }

    [HttpDelete]
    public IActionResult DeleteCategory(int id)
    {
        var lookingForCategory = service.GetById(id);
        if (lookingForCategory == null)
            return NotFound();

        service.DeleteCategory(id);

        return NoContent();
    }
}