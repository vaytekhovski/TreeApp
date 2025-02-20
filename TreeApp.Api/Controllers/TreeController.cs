using Microsoft.AspNetCore.Mvc;
using TreeApp.Application.Interfaces;

namespace TreeApp.Api.Controllers;

[Route("api.user.tree")]
[ApiController]
public class TreeController : ControllerBase
{
    private readonly ITreeService TreeService;

    public TreeController(ITreeService treeService) => TreeService = treeService;

    [HttpPost("get")]
    public async Task<IActionResult> GetTree([FromQuery] string treeName)
        => Ok(await TreeService.GetTreeAsync(treeName));

    [HttpPost("node.create")]
    public async Task<IActionResult> CreateNode([FromQuery] string treeName, [FromQuery] Guid parentNodeId, [FromQuery] string nodeName)
    {
        await TreeService.CreateNodeAsync(treeName, parentNodeId, nodeName);
        return Ok();
    }

    [HttpPost("node.delete")]
    public async Task<IActionResult> DeleteNode([FromQuery] string treeName, [FromQuery] Guid nodeId)
    {
        await TreeService.DeleteNodeAsync(treeName, nodeId);
        return Ok();
    }

    [HttpPost("node.rename")]
    public async Task<IActionResult> RenameNode([FromQuery] string treeName, [FromQuery] Guid nodeId, [FromQuery] string newNodeName)
    {
        await TreeService.RenameNodeAsync(treeName, nodeId, newNodeName);
        return Ok();
    }
}