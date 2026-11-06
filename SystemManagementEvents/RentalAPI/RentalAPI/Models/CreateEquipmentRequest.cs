using System.ComponentModel.DataAnnotations;

namespace RentalApi.Models;

public class CreateEquipmentRequest
{
    [Required(ErrorMessage = "Укажите название")]
    [StringLength(100, MinimumLength = 2,
        ErrorMessage = "Название — от 2 до 100 символов")]
    public string Name { get; set; } = "";

    [Required(ErrorMessage = "Укажите категорию")]
    public string Category { get; set; } = "";

    [Range(1, 100_000, ErrorMessage = "Цена за сутки — от 1 до 100 000")]
    public decimal PricePerDay { get; set; }

    [Range(0, 500_000, ErrorMessage = "Депозит — от 0 до 500 000")]
    public decimal Deposit { get; set; }
}