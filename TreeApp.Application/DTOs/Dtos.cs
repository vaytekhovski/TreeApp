namespace TreeApp.Application.DTOs;
public record NodeDto(Guid Id, string Name, List<NodeDto> Children);
public record JournalDto(Guid Id, string EventId, DateTime CreatedAt, string Text);
public record JournalInfoDto(Guid Id, string EventId, DateTime CreatedAt);
public record RangeDto<T>(int Skip, int Count, List<T> Items);
public record JournalFilterDto(DateTime? From, DateTime? To, string Search);