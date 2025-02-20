namespace TreeApp.Domain.Entities;
public class Tree
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public required string Name { get; set; }
    public List<Node> Nodes { get; set; } = new();
}