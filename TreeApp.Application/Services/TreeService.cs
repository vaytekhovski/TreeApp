using TreeApp.Application.DTOs;
using TreeApp.Application.Extensions;
using TreeApp.Application.Interfaces;
using TreeApp.Domain.Constants;
using TreeApp.Domain.Entities;
using TreeApp.Domain.Exceptions;

namespace TreeApp.Application.Services;
public class TreeService : ITreeService
{
    private readonly ITreeRepository Repository;
    private readonly INodeValidator Validator;

    public TreeService(ITreeRepository repository, INodeValidator validator)
    {
        Repository = repository;
        Validator = validator;
    }

    public async Task<NodeDto> GetTreeAsync(string treeName)
    {
        var tree = await Repository.GetTreeByNameAsync(treeName)
            ?? await CreateNewTreeAsync(treeName);

        var rootNode = tree.Nodes.FirstOrDefault(n => n.ParentId == null)
            ?? throw new SecureException(ErrorMessages.RootNodeNotFound);

        return rootNode.ToDto(tree.Nodes);
    }

    public async Task CreateNodeAsync(string treeName, Guid parentNodeId, string nodeName)
    {
        var tree = await GetExistingTreeAsync(treeName);
        await Validator.ValidateNodeCreationAsync(tree, parentNodeId, nodeName);

        var node = new Node { Name = nodeName, TreeId = tree.Id, ParentId = parentNodeId };
        await Repository.AddNodeAsync(node);
        await Repository.SaveChangesAsync();
    }

    public async Task DeleteNodeAsync(string treeName, Guid nodeId)
    {
        var tree = await GetExistingTreeAsync(treeName);
        var node = await GetNodeFromTreeAsync(tree, nodeId);
        await Validator.ValidateNodeDeletionAsync(node);

        await Repository.RemoveNodeAsync(node);
        await Repository.SaveChangesAsync();
    }

    public async Task RenameNodeAsync(string treeName, Guid nodeId, string newNodeName)
    {
        var tree = await GetExistingTreeAsync(treeName);
        var node = await GetNodeFromTreeAsync(tree, nodeId);
        await Validator.ValidateNodeRenamingAsync(node, newNodeName);

        node.Name = newNodeName;
        await Repository.SaveChangesAsync();
    }

    private async Task<Tree> CreateNewTreeAsync(string treeName)
    {
        var tree = new Tree { Name = treeName };
        var rootNode = new Node { Name = treeName, TreeId = tree.Id };
        tree.Nodes.Add(rootNode);
        await Repository.AddTreeAsync(tree);
        await Repository.SaveChangesAsync();
        return tree;
    }

    private async Task<Tree> GetExistingTreeAsync(string treeName)
        => await Repository.GetTreeByNameAsync(treeName)
            ?? throw new SecureException(ErrorMessages.TreeNotFound);

    private async Task<Node> GetNodeFromTreeAsync(Tree tree, Guid nodeId)
        => await Repository.GetNodeByIdAsync(nodeId) is { } node && node.TreeId == tree.Id
            ? node
            : throw new SecureException(ErrorMessages.NodeNotFound);
}