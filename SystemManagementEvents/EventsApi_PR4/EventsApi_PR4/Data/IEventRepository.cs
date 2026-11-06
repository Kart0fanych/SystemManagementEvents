using EventsApi.Models;

namespace EventsApi.Data;

public interface IEventRepository
{
    Task<IReadOnlyList<Event>> GetAllAsync(
        CancellationToken cancellationToken = default);

    Task<Event?> GetByIdAsync(
        int id, CancellationToken cancellationToken = default);

    Task<Event> AddAsync(
        Event item, CancellationToken cancellationToken = default);

    Task<bool> DeleteAsync(
        int id, CancellationToken cancellationToken = default);
}