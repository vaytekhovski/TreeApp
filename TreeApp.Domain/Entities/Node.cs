namespace TreeApp.Domain.Entities;
public class Node
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public required string Name { get; set; }
    public Guid TreeId { get; set; }
    public Guid? ParentId { get; set; }
    public Tree Tree { get; set; } = null!;
    public Node? Parent { get; set; }
    public List<Node> Children { get; set; } = new();
}