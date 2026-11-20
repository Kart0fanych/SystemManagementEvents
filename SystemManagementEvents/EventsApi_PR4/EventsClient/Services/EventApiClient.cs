using System.Net.Http.Json;
using EventsClient.Models;

namespace EventsClient.Services;

public class EventApiClient
{
    private readonly HttpClient _httpClient;

    public EventApiClient()
    {
        _httpClient = new HttpClient
        {
            BaseAddress = new Uri("http://localhost:5289/")
        };

        _httpClient.Timeout = TimeSpan.FromSeconds(10);
    }

    public async Task<PagedResult<EventListItemDTO>> GetEventsAsync(
        int page,
        int pageSize,
        CancellationToken cancellationToken = default)
    {
        return await _httpClient.GetFromJsonAsync<PagedResult<EventListItemDTO>>(
            $"api/events?page={page}&pageSize={pageSize}",
            cancellationToken)
            ?? new PagedResult<EventListItemDTO>();
    }

    public async Task<HttpResponseMessage> CreateEventAsync(
        CreateEventRequestDTO request,
        CancellationToken cancellationToken = default)
    {
        return await _httpClient.PostAsJsonAsync(
            "api/events", request, cancellationToken);
    }
}