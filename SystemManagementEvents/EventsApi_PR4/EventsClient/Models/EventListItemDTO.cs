namespace EventsClient.Models;

public class EventListItemDTO
{
    public int Id { get; set; }
    public string Title { get; set; } = "";
    public DateTime Date { get; set; }
    public string Room { get; set; } = "";
    public int MaxParticipants { get; set; }
    public string Status { get; set; } = "";
}