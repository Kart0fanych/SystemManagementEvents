using RentalApi.Models;

namespace RentalApi.Data;

public class InMemoryEquipmentRepository : IEquipmentRepository
{
    private readonly List<Equipment> _items =
    [
        new() { Id = 1, Name = "Палатка 3-местная", Category = "Палатки",
                PricePerDay = 500m, Deposit = 3000m },
        new() { Id = 2, Name = "Рюкзак 60 л", Category = "Рюкзаки",
                PricePerDay = 200m, Deposit = 1500m },
        new() { Id = 3, Name = "Спальник −10 °C", Category = "Спальники",
                PricePerDay = 250m, Deposit = 2000m }
    ];

    private readonly object _sync = new();
    private int _nextId = 4;

    public Task<IReadOnlyList<Equipment>> GetAllAsync(
        CancellationToken cancellationToken = default)
    {
        lock (_sync)
        {
            return Task.FromResult<IReadOnlyList<Equipment>>(_items.ToList());
        }
    }

    public Task<Equipment?> GetByIdAsync(
        int id, CancellationToken cancellationToken = default)
    {
        lock (_sync)
        {
            return Task.FromResult(_items.FirstOrDefault(e => e.Id == id));
        }
    }

    public Task<Equipment> AddAsync(
        Equipment equipment, CancellationToken cancellationToken = default)
    {
        lock (_sync)
        {
            equipment.Id = _nextId++;
            _items.Add(equipment);
            return Task.FromResult(equipment);
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