using TreeApp.Domain.Entities;

namespace TreeApp.Application.Interfaces;
public interface ITreeRepository
{
    Task<Tree?> GetTreeByNameAsync(string name);
    Task<Node?> GetNodeByIdAsync(Guid id);
    Task AddTreeAsync(Tree tree);
    Task AddNodeAsync(Node node);
    Task RemoveNodeAsync(Node node);
    Task SaveChangesAsync();
}