
using Microsoft.Extensions.Caching.Memory;
using System.Diagnostics;

namespace EventsApi.Services;

public class EventStatusService
{
    private const string CacheKey = "EventStatuses";

    private readonly IMemoryCache _cache;
    private readonly ILogger<EventStatusService> _logger;

    public EventStatusService(
        IMemoryCache cache,
        ILogger<EventStatusService> logger)
    {
        _cache = cache;
        _logger = logger;
    }

    public IReadOnlyList<string> GetStatuses()
    {
        var timer = Stopwatch.StartNew();

        bool cacheHit = _cache.TryGetValue(
            CacheKey, out IReadOnlyList<string>? statuses);

        if (!cacheHit)
        {
            statuses = _cache.GetOrCreate(CacheKey, entry =>
            {
                entry.AbsoluteExpirationRelativeToNow =
                    TimeSpan.FromMinutes(30);

                return new List<string>
                {
                    "Черновик",
                    "Опубликовано",
                    "Завершено"
                };
            });
        }

        timer.Stop();

        _logger.LogInformation(
            "Получение справочника: {Time:F4} мс. Кэш: {CacheStatus}",
            timer.Elapsed.TotalMilliseconds,
            cacheHit ? "Попадание" : "Промах");

        return statuses!;
    }
}
