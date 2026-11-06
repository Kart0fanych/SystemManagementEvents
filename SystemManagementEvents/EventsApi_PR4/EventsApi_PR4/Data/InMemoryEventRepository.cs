using EventsApi.Models;

namespace EventsApi.Data;

public class InMemoryEventRepository : IEventRepository
{
    private readonly List<Event> _items =
    [
        new()
        {
            Id = 1,
            Title = "День открытых дверей",
            Date = new DateTime(2026, 11, 15),
            Room = "Актовый зал",
            MaxParticipants = 200,
            Status = "Опубликовано"
        },
        new()
        {
            Id = 2,
            Title = "Научная конференция студентов",
            Date = new DateTime(2026, 12, 5),
            Room = "Аудитория 214",
            MaxParticipants = 80,
            Status = "Черновик"
        },
        new()
        {
            Id = 3,
            Title = "Встреча с работодателями",
            Date = new DateTime(2026, 11, 20),
            Room = "Аудитория 101",
            MaxParticipants = 50,
            Status = "Опубликовано"
        }
    ];

    private readonly object _sync = new();
    private int _nextId = 4;

    public Task<IReadOnlyList<Event>> GetAllAsync(
        CancellationToken cancellationToken = default)
    {
        lock (_sync)
        {
            return Task.FromResult<IReadOnlyList<Event>>(_items.ToList());
        }
    }

    public Task<Event?> GetByIdAsync(
        int id, CancellationToken cancellationToken = default)
    {
        lock (_sync)
        {
            return Task.FromResult(_items.FirstOrDefault(e => e.Id == id));
        }
    }

    public Task<Event> AddAsync(
        Event item, CancellationToken cancellationToken = default)
    {
        lock (_sync)
        {
            item.Id = _nextId++;
            _items.Add(item);
            return Task.FromResult(item);
        }
    }

    public Task<bool> DeleteAsync(
        int id, CancellationToken cancellationToken = default)
    {
        lock (_sync)
        {
            int removed = _items.RemoveAll(e => e.Id == id);
            return Task.FromResult(removed > 0);
        }
    }
}