using EventsApi.Models;
using System.Diagnostics;
namespace EventsApi.Data;

public class InMemoryEventRepository : IEventRepository
{
    private readonly Dictionary<int, Event> _byId = new();
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

    public InMemoryEventRepository()
    {
        for (int i = 4; i <= 2000; i++)
        {
            _items.Add(new Event
            {
                Id = i,
                Title = $"Тестовое мероприятие № {i}",
                Date = new DateTime(2026, 1, 1).AddDays(i % 365),
                Room = $"Аудитория {100 + i % 50}",
                MaxParticipants = 20 + i % 181,
                Status = i % 3 == 0
                    ? "Опубликовано"
                    : i % 3 == 1
                        ? "Черновик"
                        : "Завершено",
                CreatedAt = DateTime.Now.AddMinutes(-i)
            });
        }

        foreach (var item in _items)
        {
            _byId[item.Id] = item;
        }

        _nextId = 2001;
    }

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
            _byId.TryGetValue(id, out var item);
            return Task.FromResult(item);
        }
    }

    public Task<Event> AddAsync(
    Event item, CancellationToken cancellationToken = default)
    {
        lock (_sync)
        {
            item.Id = _nextId++;
            _items.Add(item);
            _byId[item.Id] = item;

            return Task.FromResult(item);
        }
    }

    public Task<bool> DeleteAsync(
    int id, CancellationToken cancellationToken = default)
    {
        lock (_sync)
        {
            if (!_byId.TryGetValue(id, out var item))
                return Task.FromResult(false);

            _byId.Remove(id);
            _items.Remove(item);

            return Task.FromResult(true);
        }
    }
    public Task<int> CountAsync(
    CancellationToken cancellationToken = default)
    {
        lock (_sync)
        {
            return Task.FromResult(_items.Count);
        }
    }

    public Task<IReadOnlyList<Event>> GetPageAsync(
        int page,
        int pageSize,
        CancellationToken cancellationToken = default)
    {
        lock (_sync)
        {
            IReadOnlyList<Event> items = _items
                .OrderBy(e => e.Date)
                .ThenBy(e => e.Id)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return Task.FromResult(items);
        }
    }
}