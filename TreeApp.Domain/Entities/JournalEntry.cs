namespace TreeApp.Domain.Entities;
public class JournalEntry
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public required string EventId { get; set; } = Guid.NewGuid().ToString();
    public DateTime CreatedAt { get; set; }
    public string? QueryParams { get; set; }
    public string? BodyParams { get; set; }
    public required string StackTrace { get; set; }
}