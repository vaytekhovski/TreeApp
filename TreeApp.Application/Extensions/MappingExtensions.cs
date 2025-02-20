using TreeApp.Application.DTOs;
using TreeApp.Domain.Entities;

namespace TreeApp.Application.Extensions;
public static class MappingExtensions
{
    public static NodeDto ToDto(this Node node, IEnumerable<Node> allNodes)
    {
        return new NodeDto(
            node.Id,
            node.Name,
            allNodes.Where(n => n.ParentId == node.Id)
                .Select(n => n.ToDto(allNodes))
                .ToList()
        );
    }

    public static JournalDto ToDto(this JournalEntry entry)
    {
        return new JournalDto(
            entry.Id,
            entry.EventId,
            entry.CreatedAt,
            $"Query: {entry.QueryParams}\nBody: {entry.BodyParams}\nStack: {entry.StackTrace}"
        );
    }

    public static JournalInfoDto ToInfoDto(this JournalEntry entry)
    {
        return new JournalInfoDto(entry.Id, entry.EventId, entry.CreatedAt);
    }
}