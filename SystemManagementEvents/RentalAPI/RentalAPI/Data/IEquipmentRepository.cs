using RentalApi.Models;

namespace RentalApi.Data;

public interface IEquipmentRepository
{
    Task<IReadOnlyList<Equipment>> GetAllAsync(
        CancellationToken cancellationToken = default);

    Task<Equipment?> GetByIdAsync(
        int id, CancellationToken cancellationToken = default);

    Task<Equipment> AddAsync(
        Equipment equipment, CancellationToken cancellationToken = default);

    Task<bool> DeleteAsync(
        int id, CancellationToken cancellationToken = default);
}