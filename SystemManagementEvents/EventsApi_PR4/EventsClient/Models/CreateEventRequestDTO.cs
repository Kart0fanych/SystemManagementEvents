using EventsClient.Models;

public class CreateEventRequestDTO
{
    public string Title { get; set; } = "";
    public DateTime Date { get; set; }
    public string Room { get; set; } = "";
    public int MaxParticipants { get; set; }
}