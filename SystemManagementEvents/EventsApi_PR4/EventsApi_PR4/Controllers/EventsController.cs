using Microsoft.AspNetCore.Mvc;
using EventsApi.Data;
using EventsApi.Models;

namespace EventsApi.Controllers;

/// <summary>
/// Предоставляет методы для просмотра и управления мероприятиями.
/// </summary>
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

    /// <summary>
    /// Получает список всех мероприятий.
    /// </summary>
    /// <returns>Список мероприятий.</returns>
    /// <response code="200">Список успешно получен.</response>
    [HttpGet]
    [ProducesResponseType(typeof(List<Event>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<Event>>> GetAll(
        CancellationToken cancellationToken)
    {
        var items = await _repository.GetAllAsync(cancellationToken);
        return Ok(items);
    }

    /// <summary>
    /// Получает информацию о мероприятии по его идентификатору.
    /// </summary>
    /// <param name="id">Уникальный идентификатор мероприятия.</param>
    /// <param name="cancellationToken">Токен отмены запроса.</param>
    /// <returns>Информация о найденном мероприятии.</returns>
    /// <response code="200">Мероприятие найдено.</response>
    /// <response code="404">Мероприятие с указанным идентификатором не найдено.</response>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(Event), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Event>> GetById(
        int id, CancellationToken cancellationToken)
    {
        var item = await _repository.GetByIdAsync(id, cancellationToken);

        if (item is null)
            return NotFound();

        return Ok(item);
    }

    /// <summary>
    /// Создаёт новое мероприятие.
    /// </summary>
    /// <param name="request">Данные нового мероприятия.</param>
    /// <param name="cancellationToken">Токен отмены запроса.</param>
    /// <returns>Созданное мероприятие.</returns>
    /// <response code="201">Мероприятие успешно создано.</response>
    /// <response code="400">Переданы некорректные данные.</response>
    /// <response code="409">Аудитория уже занята мероприятием на указанную дату.</response>
    [HttpPost]
    [ProducesResponseType(typeof(Event), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(string), StatusCodes.Status409Conflict)]
    public async Task<ActionResult<Event>> Create(
        CreateEventRequest request,
        CancellationToken cancellationToken)
    {
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

        _logger.LogInformation(
            "Создано мероприятие {EventId}", created.Id);

        return CreatedAtAction(
            nameof(GetById),
            new { id = created.Id },
            created);
    }

    /// <summary>
    /// Удаляет мероприятие по его идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор удаляемого мероприятия.</param>
    /// <param name="cancellationToken">Токен отмены запроса.</param>
    /// <response code="204">Мероприятие успешно удалено.</response>
    /// <response code="404">Мероприятие не найдено.</response>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(
        int id, CancellationToken cancellationToken)
    {
        bool deleted = await _repository.DeleteAsync(id, cancellationToken);

        return deleted ? NoContent() : NotFound();
    }
}