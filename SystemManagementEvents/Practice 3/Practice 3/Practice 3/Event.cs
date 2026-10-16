
using System;
using System.ComponentModel.DataAnnotations;

namespace Practice_3
{
    public enum EventStatus
    {
        Draft,
        Published,
        Completed
    }

    internal class Event
    {
        [Range(1, int.MaxValue)]
        public int Id { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string Name { get; set; } = string.Empty;

        [Range(typeof(DateTime), "2026-01-01", "2035-12-31")]
        public DateTime Date { get; set; }

        [EnumDataType(typeof(EventStatus))]
        public EventStatus Status { get; set; }

        public Event()
        {
            Status = EventStatus.Draft;
        }

        public Event(int id, string name, DateTime date, EventStatus status)
        {
            Id = id;
            Name = name;
            Date = date;
            Status = status;
        }

        public Event(Event other)
        {
            ArgumentNullException.ThrowIfNull(other);

            Id = other.Id;
            Name = other.Name;
            Date = other.Date;
            Status = other.Status;
        }
    }
}
