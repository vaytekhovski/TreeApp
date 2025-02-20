using TreeApp.Application.DTOs;

namespace TreeApp.Application.Interfaces;
public interface IJournalService
{
    Task<RangeDto<JournalInfoDto>> GetJournalRangeAsync(int skip, int take, JournalFilterDto filter);
    Task<JournalDto> GetJournalEntryAsync(Guid id);
}