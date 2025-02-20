using Microsoft.EntityFrameworkCore;
using TreeApp.Domain.Entities;
using TreeApp.Application.Interfaces;

namespace TreeApp.Infrastructure.Data;
public class TreeRepository : ITreeRepository
{
    private readonly ApplicationDbContext Context;

    public TreeRepository(ApplicationDbContext context) => Context = context;

    public async Task<Tree?> GetTreeByNameAsync(string name)
        => await Context.Trees
            .Include(t => t.Nodes)
            .ThenInclude(n => n.Children)
            .FirstOrDefaultAsync(t => t.Name == name);

    public async Task<Node?> GetNodeByIdAsync(Guid id)
        => await Context.Nodes
            .Include(n => n.Children)
            .Include(n => n.Parent)
            .FirstOrDefaultAsync(n => n.Id == id);

    public Task AddTreeAsync(Tree tree) => Task.FromResult(Context.Trees.Add(tree));
    public Task AddNodeAsync(Node node) => Task.FromResult(Context.Nodes.Add(node));
    public Task RemoveNodeAsync(Node node) => Task.FromResult(Context.Nodes.Remove(node));
    public Task SaveChangesAsync() => Context.SaveChangesAsync();
}