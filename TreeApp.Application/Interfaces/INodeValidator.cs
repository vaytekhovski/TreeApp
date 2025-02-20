using TreeApp.Domain.Entities;

namespace TreeApp.Application.Interfaces;

public interface INodeValidator
{
    Task ValidateNodeCreationAsync(Tree tree, Guid parentNodeId, string nodeName);
    Task ValidateNodeDeletionAsync(Node node);
    Task ValidateNodeRenamingAsync(Node node, string newName);
}