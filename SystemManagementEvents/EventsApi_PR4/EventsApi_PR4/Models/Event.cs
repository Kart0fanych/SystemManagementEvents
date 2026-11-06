namespace EventsApi.Models;

public class Event
{
    public int Id { get; set; }
    public string Title { get; set; } = "";
    public DateTime Date { get; set; }
    public string Room { get; set; } = "";
    public int MaxParticipants { get; set; }
    public string Status { get; set; } = "Черновик";
    public DateTime CreatedAt { get; set; } = DateTime.Now;
}