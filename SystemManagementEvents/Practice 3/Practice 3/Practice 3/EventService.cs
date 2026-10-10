using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Practice_3
{
    internal class EventService
    {
        private int _nextId = 1;
        private List<Event> _events = new List<Event>();
        private readonly ILogger<EventService> _logger;
        public EventService(ILogger<EventService> logger)
        {
            _logger = logger;
        }
        public Event CreateEvent(string name, DateTime date)
        {
            Event eventItem = new Event(_nextId, name, date, EventStatus.Draft);
            if (!ValidateEvent(eventItem))
            {
                return null;
            }
            _nextId++;
            _events.Add(eventItem);
            _logger.LogInformation("Создано мероприятие с ID {EventId}", eventItem.Id);
            return eventItem;
        }
        public void EditEvent(int id, string name, DateTime date)
        {
            Event foundEvent = null;

            foreach (Event item in _events)
            {
                if (item.Id == id)
                {
                    foundEvent = item;
                    break;
                }
            }

            if (foundEvent == null)
            {
                _logger.LogWarning(
                    "Мероприятие с ID {EventId} не найдено", id);

                throw new EventNotFoundException(
                    $"Мероприятие с ID {id} не найдено.");
            }

            Event updatedEvent = new Event(
                foundEvent.Id,
                name,
                date,
                foundEvent.Status
            );

            if (!ValidateEvent(updatedEvent))
            {
                _logger.LogWarning(
                    "Редактирование мероприятия с ID {EventId} отменено",
                    id);
                return;
            }

            foundEvent.Name = name;
            foundEvent.Date = date;

            _logger.LogInformation("Изменено мероприятие с ID {EventId}", id);
        }

        private bool ValidateEvent(Event eventItem)
        {
            ValidationContext context = new ValidationContext(eventItem);
            List<ValidationResult> results = new List<ValidationResult>();

            bool isValid = Validator.TryValidateObject(
                eventItem,
                context,
                results,
                validateAllProperties: true
            );

            if (!isValid)
            {
                foreach (ValidationResult error in results)
                {
                    _logger.LogWarning(
                        "Ошибка валидации мероприятия: {ValidationError}",
                        error.ErrorMessage);
                }
            }

            return isValid;
        }
        public void CompleteEvent(int id)
        {
            try
            {
                Event foundEvent = _events.Find(item => item.Id == id);

                if (foundEvent == null)
                {
                    throw new EventNotFoundException(
                        $"Мероприятие с ID {id} не найдено.");
                }

                if (foundEvent.Status != EventStatus.Published)
                {
                    throw new InvalidEventStatusException(
                        $"Нельзя завершить мероприятие со статусом {foundEvent.Status}.");
                }

                foundEvent.Status = EventStatus.Completed;

                _logger.LogInformation("Мероприятие {EventId} завершено", foundEvent.Id);
            }
            catch (EventNotFoundException ex)
            {
                _logger.LogWarning(
                    ex,
                    "Не удалось завершить мероприятие {EventId}: оно не найдено",
                    id);

                throw;
            }
            catch (InvalidEventStatusException ex)
            {
                _logger.LogWarning(
                    ex,
                    "Не удалось завершить мероприятие {EventId}: недопустимый статус",
                    id);

                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex, "Непредвиденная ошибка при завершении мероприятия {EventId}", id);

                throw;
            }
            finally
            {
                _logger.LogInformation("Попытка завершения мероприятия {EventId} обработана", id);
            }
        }

        public void PublishEvent(int id)
        {
            Event foundEvent = _events.Find(item => item.Id == id);

            if (foundEvent == null)
            {
                throw new EventNotFoundException(
                    $"Мероприятие с ID {id} не найдено.");
            }

            foundEvent.Status = EventStatus.Published;

            _logger.LogInformation(
                "Мероприятие {EventId} опубликовано",
                id);
        }

    }
}
