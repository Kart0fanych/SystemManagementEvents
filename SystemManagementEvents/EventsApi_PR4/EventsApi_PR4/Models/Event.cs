namespace EventsApi.Models;

/// <summary>
/// Представляет мероприятие образовательной организации.
/// </summary>
public class Event
{
    /// <summary>
    /// Уникальный идентификатор мероприятия.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Название мероприятия.
    /// </summary>
    public string Title { get; set; } = "";

    /// <summary>
    /// Дата проведения мероприятия.
    /// </summary>
    public DateTime Date { get; set; }

    /// <summary>
    /// Аудитория, в которой проводится мероприятие.
    /// </summary>
    public string Room { get; set; } = "";

    /// <summary>
    /// Максимальное количество участников.
    /// </summary>
    public int MaxParticipants { get; set; }

    /// <summary>
    /// Текущий статус мероприятия.
    /// </summary>
    public string Status { get; set; } = "Черновик";

    /// <summary>
    /// Дата и время создания записи.
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.Now;
}