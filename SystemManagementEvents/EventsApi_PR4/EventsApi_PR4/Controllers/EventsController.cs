using Microsoft.AspNetCore.Mvc;
using EventsApi.Data;
using EventsApi.Models;

namespace EventsApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EventsController : ControllerBase
{
    private readonly IEventRepository _repository;
    private readonly ILogger<EventsController> _logger;

    public EventsController(
        IEventRepository repository,
        ILogger<EventsController> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    // GET /api/events
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<Event>>> GetAll(
        CancellationToken cancellationToken)
    {
        var items = await _repository.GetAllAsync(cancellationToken);
        return Ok(items);
    }

    // GET /api/events/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Event>> GetById(
        int id, CancellationToken cancellationToken)
    {
        var item = await _repository.GetByIdAsync(id, cancellationToken);
        if (item is null)
            return NotFound();

        return Ok(item);
    }

    // POST /api/events
    [HttpPost]
    public async Task<ActionResult<Event>> Create(
        CreateEventRequest request,
        CancellationToken cancellationToken)
    {
        // Бизнес-правило: нельзя два мероприятия в одной аудитории в один день
        var all = await _repository.GetAllAsync(cancellationToken);
        bool conflict = all.Any(e =>
            e.Room.Equals(request.Room, StringComparison.OrdinalIgnoreCase) &&
            e.Date.Date == request.Date!.Value.Date);

        if (conflict)
        {
            return Conflict(
                $"Аудитория «{request.Room}» уже занята " +
                $"мероприятием на {request.Date:dd.MM.yyyy}");
        }

        var item = new Event
        {
            Title = request.Title,
            Date = request.Date!.Value,
            Room = request.Room,
            MaxParticipants = request.MaxParticipants
        };

        var created = await _repository.AddAsync(item, cancellationToken);
        _logger.LogInformation("Создано мероприятие {EventId}", created.Id);

        return CreatedAtAction(
            nameof(GetById),
            new { id = created.Id },
            created);
    }

    // DELETE /api/events/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(
        int id, CancellationToken cancellationToken)
    {
        bool deleted = await _repository.DeleteAsync(id, cancellationToken);
        return deleted ? NoContent() : NotFound();
    }
}