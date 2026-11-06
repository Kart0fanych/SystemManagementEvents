namespace RentalApi.Models;

public class Equipment
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public string Category { get; set; } = "";
    public decimal PricePerDay { get; set; }
    public decimal Deposit { get; set; }
    public bool IsAvailable { get; set; } = true;
}