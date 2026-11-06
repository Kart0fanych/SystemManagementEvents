using Microsoft.AspNetCore.Mvc;
using RentalApi.Data;
using RentalApi.Models;

namespace RentalApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EquipmentController : ControllerBase
{
    private readonly IEquipmentRepository _repository;
    private readonly ILogger<EquipmentController> _logger;

    public EquipmentController(
        IEquipmentRepository repository,
        ILogger<EquipmentController> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    // GET /api/equipment
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<Equipment>>> GetAll(
        CancellationToken cancellationToken)
    {
        var items = await _repository.GetAllAsync(cancellationToken);
        return Ok(items);
    }

    // GET /api/equipment/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Equipment>> GetById(
        int id, CancellationToken cancellationToken)
    {
        var item = await _repository.GetByIdAsync(id, cancellationToken);
        if (item is null)
            return NotFound();

        return Ok(item);
    }

    // POST /api/equipment
    [HttpPost]
    public async Task<ActionResult<Equipment>> Create(
        CreateEquipmentRequest request,
        CancellationToken cancellationToken)
    {
        var equipment = new Equipment
        {
            Name = request.Name,
            Category = request.Category,
            PricePerDay = request.PricePerDay,
            Deposit = request.Deposit
        };

        var created = await _repository.AddAsync(equipment, cancellationToken);
        _logger.LogInformation("Добавлено снаряжение {EquipmentId}", created.Id);

        return CreatedAtAction(
            nameof(GetById),
            new { id = created.Id },
            created);
    }

    // DELETE /api/equipment/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(
        int id, CancellationToken cancellationToken)
    {
        bool deleted = await _repository.DeleteAsync(id, cancellationToken);
        return deleted ? NoContent() : NotFound();
    }
}