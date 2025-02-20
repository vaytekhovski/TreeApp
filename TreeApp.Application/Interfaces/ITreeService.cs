using TreeApp.Application.DTOs;

namespace TreeApp.Application.Interfaces;
public interface ITreeService
{
    Task<NodeDto> GetTreeAsync(string treeName);
    Task CreateNodeAsync(string treeName, Guid parentNodeId, string nodeName);
    Task DeleteNodeAsync(string treeName, Guid nodeId);
    Task RenameNodeAsync(string treeName, Guid nodeId, string newNodeName);
}