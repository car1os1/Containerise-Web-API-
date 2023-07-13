using DemoApp.Data;
using DemoApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DemoApp.Controllers;

[ApiController]
[Route("[controller]")]
public class DriversController : ControllerBase
{

    private readonly ILogger<DriversController> _logger;
    private readonly ApiDbContext _context;

    public DriversController(ILogger<DriversController> logger, ApiDbContext context)
    {
        _logger = logger;
        _context = context;

    }

    [HttpGet(Name = "GetAllDrivers")]
    public async Task<IActionResult> Get()
    {
        var driver = new Driver()
        {
            DriverNumber = 44,
            Name = "Carlos Chullunquia"
        };

        _context.Add(driver);
        await _context.SaveChangesAsync();

        var allDrivers = await _context.Drives.ToListAsync();

        return Ok(allDrivers);

    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var driver = await _context.Drives.FindAsync(id);
        if (driver == null)
        {
            return NotFound(); // Devuelve una respuesta 404 si no se encuentra el conductor
        }

        _context.Drives.Remove(driver);
        await _context.SaveChangesAsync(); // Guarda los cambios en la base de datos

        return NoContent(); // Devuelve una respuesta 204 indicando éxito sin contenido
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] Driver updatedDriver)
    {
        var driver = await _context.Drives.FindAsync(id);
        if (driver == null)
        {
            return NotFound(); // Devuelve una respuesta 404 si no se encuentra el conductor
        }

        driver.DriverNumber = updatedDriver.DriverNumber;
        driver.Name = updatedDriver.Name;

        await _context.SaveChangesAsync(); // Guarda los cambios en la base de datos

        return Ok(driver);
    }

}
