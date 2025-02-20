using System.Text.Json;
using TreeApp.Application.Interfaces;
using TreeApp.Domain.Entities;
using TreeApp.Domain.Exceptions;

namespace TreeApp.Api.Middleware;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate Next;
    private readonly IServiceScopeFactory ServiceScopeFactory;

    public ExceptionHandlingMiddleware(RequestDelegate next, IServiceScopeFactory serviceScopeFactory)
    {
        Next = next;
        ServiceScopeFactory = serviceScopeFactory;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await Next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var eventId = Guid.NewGuid().ToString();

        using (var scope = ServiceScopeFactory.CreateScope())
        {
            var repository = scope.ServiceProvider.GetRequiredService<IJournalRepository>();

            var journalEntry = new JournalEntry
            {
                EventId = eventId,
                CreatedAt = DateTime.UtcNow,
                QueryParams = JsonSerializer.Serialize(context.Request.Query),
                BodyParams = await GetBodyAsync(context.Request),
                StackTrace = exception.StackTrace ?? string.Empty
            };

            await repository.AddJournalEntryAsync(journalEntry);
            await repository.SaveChangesAsync();
        }

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = StatusCodes.Status500InternalServerError;

        object response = exception switch
        {
            SecureException secureEx => new { type = secureEx.GetType().Name, id = eventId, data = new { message = secureEx.Message } },
            _ => new { type = "Exception", id = eventId, data = new { message = $"Internal server error ID = {eventId}" } }
        };

        await context.Response.WriteAsync(JsonSerializer.Serialize(response));
    }

    private static async Task<string> GetBodyAsync(HttpRequest request)
    {
        request.EnableBuffering();
        using var reader = new StreamReader(request.Body, leaveOpen: true);
        var body = await reader.ReadToEndAsync();
        request.Body.Position = 0;
        return body;
    }
}
