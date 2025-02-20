using TreeApp.Application.Interfaces;
using TreeApp.Domain.Constants;
using TreeApp.Domain.Entities;
using TreeApp.Domain.Exceptions;

namespace TreeApp.Application.Services;
public class NodeValidator : INodeValidator
{
    private readonly ITreeRepository Repository;

    public NodeValidator(ITreeRepository repository) => Repository = repository;

    public async Task ValidateNodeCreationAsync(Tree tree, Guid parentNodeId, string nodeName)
    {
        var parent = await Repository.GetNodeByIdAsync(parentNodeId)
            ?? throw new SecureException(ErrorMessages.ParentNodeNotFound);

        if (parent.TreeId != tree.Id)
            throw new SecureException(ErrorMessages.ParentNodeDifferentTree);

        if (parent.Children.Any(c => c.Name == nodeName))
            throw new SecureException(ErrorMessages.DuplicateNodeName);
    }

    public async Task ValidateNodeDeletionAsync(Node node)
    {
        if (node.Children.Count != 0)
            throw new SecureException(ErrorMessages.HasChildren);
    }

    public async Task ValidateNodeRenamingAsync(Node node, string newName)
    {
        if (node.Parent?.Children.Any(c => c.Name == newName && c.Id != node.Id) == true)
            throw new SecureException(ErrorMessages.DuplicateNodeName);
    }
}