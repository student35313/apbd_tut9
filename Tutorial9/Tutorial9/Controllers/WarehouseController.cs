using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Tutorial9.Exceptions;
using Tutorial9.Model.DTOs;
using Tutorial9.Services.Product_Warehouse;

namespace Tutorial9.Controllers;

[Route("api/[controller]")]
[ApiController]
public class WarehouseController : ControllerBase
{
    IProductWarehouseService _service;

    public WarehouseController(IProductWarehouseService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> AddProductToWarehouse(ProductWarehouseInsertDTO dto)
    {
        try
        {
            var id = await _service.AddProductToWarehouse(dto);
            return Created("", id);
        }
        catch (NotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (ConflictException ex)
        {
            return Conflict(ex.Message);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            return StatusCode(StatusCodes.Status500InternalServerError,
                new { error = "Internal server error" });
        }
    }

    [HttpPost("procedure")]
    public async Task<IActionResult> ProcedureAddProductToWarehouse(ProductWarehouseInsertDTO dto){
        try
        {
            var id = await _service.ProcedureAddProductToWarehouse(dto);
            return Created("", id);
        }
        catch (SqlException ex)
        {
            return BadRequest($"Database error: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            return StatusCode(StatusCodes.Status500InternalServerError,
                new { error = "Internal server error" });
        }
    }

}