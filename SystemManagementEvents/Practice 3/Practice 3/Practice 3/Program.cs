
using Microsoft.Extensions.Logging;
using System;

namespace Practice_3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using var loggerFactory = LoggerFactory.Create(builder =>
            {
                builder.AddConsole();
                builder.SetMinimumLevel(LogLevel.Information);
            });

            ILogger<EventService> logger =
                loggerFactory.CreateLogger<EventService>();

            EventService service = new EventService(logger);

            try
            {
                Console.WriteLine("=== 1. Создание мероприятия ===");

                Event eventItem = service.CreateEvent(
                    "День открытых дверей",
                    new DateTime(2026, 11, 20)
                );

                if (eventItem == null)
                {
                    Console.WriteLine("Создание не удалось.");
                    return;
                }

                Console.WriteLine(
                    $"Создано: ID={eventItem.Id}, " +
                    $"название={eventItem.Name}, " +
                    $"статус={eventItem.Status}");

                Console.WriteLine("\n=== 2. Проверка валидации ===");

                Event invalidEvent = service.CreateEvent(
                    "а",
                    new DateTime(2026, 11, 21)
                );

                Console.WriteLine(
                    invalidEvent == null
                        ? "Некорректное название отклонено."
                        : "ОШИБКА: некорректное название принято.");

                Console.WriteLine("\n=== 3. Проверка отсутствующего ID ===");

                try
                {
                    service.EditEvent(
                        999,
                        "Несуществующее мероприятие",
                        DateTime.Now
                    );
                }
                catch (EventNotFoundException ex)
                {
                    Console.WriteLine(
                        $"Ожидаемая ошибка: {ex.Message}");
                }

                Console.WriteLine("\n=== 4. Проверка недопустимого статуса ===");

                try
                {
                    service.CompleteEvent(eventItem.Id);
                }
                catch (InvalidEventStatusException ex)
                {
                    Console.WriteLine(
                        $"Ожидаемая ошибка: {ex.Message}");
                }

                Console.WriteLine("\n=== 5. Проверка успешного завершения ===");

                service.PublishEvent(eventItem.Id);
                service.CompleteEvent(eventItem.Id);

                Console.WriteLine(
                    $"Итоговый статус: {eventItem.Status}");

                Console.WriteLine("\n=== Проверка завершена ===");
            }
            catch (Exception ex)
            {
                logger.LogError(
                    ex,
                    "Непредвиденная ошибка при проверке EventService");

                Console.WriteLine($"Ошибка: {ex.Message}");
            }
            finally
            {
                Console.WriteLine(
                    "\nБлок finally выполнен. Проверка окончена.");
            }
        }
    }
}
