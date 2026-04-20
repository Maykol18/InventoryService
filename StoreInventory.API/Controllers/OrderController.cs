using Microsoft.AspNetCore.Mvc;
using StoreInventory.API.Models;
using StoreInventory.API.Services;

namespace StoreInventory.API.Controllers;

[ApiController]
[Route("[controller]")]
public class OrderController : ControllerBase
{
    IOrderService service = new OrderService();

    [HttpGet]
    public IActionResult GetAll()
    {
        var sales = service.GetAll();

        return Ok(sales);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var sale = service.GetById(id);

        if(sale == null)
            return NotFound();

        return Ok(sale);
    }

    [HttpPost]
    public IActionResult PlaceOrder(Order order)
    {
        service.PlaceOrder(order);

        return Created();
    }

    [HttpPut]
    public IActionResult UpdateOrder(int id, Order dto)
    {
        var sale = service.GetById(id);
        if (sale == null)
            return NotFound();
        
        var order = service.UpdateOrder(id, dto);

        return Ok(order);
    }

    [HttpDelete]
    public IActionResult CancelOrder(int id)
    {
        var sale = service.GetById(id);
        if (sale == null)
            return NotFound();
        
        service.CancelOrder(id);

        return NoContent();
    }
}