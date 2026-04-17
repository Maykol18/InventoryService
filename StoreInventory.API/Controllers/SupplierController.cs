using Microsoft.AspNetCore.Mvc;
using StoreInventory.API.Models;
using StoreInventory.API.Services;

namespace StoreInventory.API.Controllers;

[ApiController]
[Route("[controller]")]
public class SupplierController: ControllerBase
{
    ISupplierService service = new SupplierService();
    [HttpGet]
    public IActionResult GetAll()
    {
        var suppliers = service.GetAll();

        return Ok(suppliers);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var supplier = service.GetById(id);
        if(supplier == null)
            return NotFound();
        
        return Ok(supplier);
    }

    [HttpPost]
    public IActionResult AddSupplier(Supplier supplier)
    {
        if(!ModelState.IsValid)
            return BadRequest(new
            {
                Message = "Datos Invalidos",
                Errors = ModelState
            });
        service.CreateSupplier(supplier);
        return Created();
    }

    [HttpPut]
    public IActionResult UpdateSupplier(int id, Supplier dto)
    {
        var lookingForSupplier = service.GetById(id);
        if(lookingForSupplier == null)
            return NotFound();
        
        if(!ModelState.IsValid)
            return BadRequest(new
            {
                Message = "Datos Invalidos",
                Errors = ModelState
            });
        
        var supplier = service.UpdateSupplier(id, dto);

        return Ok(supplier);
    }

    [HttpDelete]
    public IActionResult DeleteSupplier(int id)
    {
        var lookingForSupplier = service.GetById(id);
        if(lookingForSupplier == null)
            return NotFound();
        
        service.DeleteSupplier(id);

        return NoContent();
    }
}