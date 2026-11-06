using System.ComponentModel.DataAnnotations;

namespace EventsApi.Models;

public class CreateEventRequest
{
    [Required(ErrorMessage = "Укажите название мероприятия")]
    [StringLength(150, MinimumLength = 5,
        ErrorMessage = "Название — от 5 до 150 символов")]
    public string Title { get; set; } = "";

    [Required(ErrorMessage = "Укажите дату мероприятия")]
    public DateTime? Date { get; set; }

    [Required(ErrorMessage = "Укажите аудиторию")]
    public string Room { get; set; } = "";

    [Range(1, 500, ErrorMessage = "Максимум участников — от 1 до 500")]
    public int MaxParticipants { get; set; }
}