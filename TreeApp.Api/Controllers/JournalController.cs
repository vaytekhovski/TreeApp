using Microsoft.AspNetCore.Mvc;
using TreeApp.Application.DTOs;
using TreeApp.Application.Interfaces;

namespace TreeApp.Api.Controllers;

[Route("api.user.journal")]
[ApiController]
public class JournalController : ControllerBase
{
    private readonly IJournalService JournalService;

    public JournalController(IJournalService journalService) => JournalService = journalService;

    [HttpPost("getRange")]
    public async Task<IActionResult> GetRange([FromQuery] int skip, [FromQuery] int take, [FromBody] JournalFilterDto filter)
        => Ok(await JournalService.GetJournalRangeAsync(skip, take, filter));

    [HttpPost("getSingle")]
    public async Task<IActionResult> GetSingle([FromQuery] Guid id)
        => Ok(await JournalService.GetJournalEntryAsync(id));
}