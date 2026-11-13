using System.ComponentModel.DataAnnotations;

namespace EventsApi.Models;

/// <summary>
/// Содержит данные для создания нового мероприятия.
/// </summary>
public class CreateEventRequest
{
    /// <summary>
    /// Название мероприятия.
    /// <example>День открытых дверей</example>
    /// </summary>
    [Required(ErrorMessage = "Укажите название мероприятия")]
    [StringLength(150, MinimumLength = 5,
        ErrorMessage = "Название — от 5 до 150 символов")]
    public string Title { get; set; } = "";

    /// <summary>
    /// Дата проведения мероприятия.
    /// <example>2026-12-15T10:00:00</example>
    /// </summary>
    [Required(ErrorMessage = "Укажите дату мероприятия")]
    public DateTime? Date { get; set; }

    /// <summary>
    /// Название аудитории.
    /// <example>Аудитория 214</example>
    /// </summary>
    [Required(ErrorMessage = "Укажите аудиторию")]
    public string Room { get; set; } = "";

    /// <summary>
    /// Максимальное количество участников.
    /// <example>80</example>
    /// </summary>
    [Range(1, 500, ErrorMessage = "Максимум участников — от 1 до 500")]
    public int MaxParticipants { get; set; }
}