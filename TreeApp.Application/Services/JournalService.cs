using TreeApp.Application.DTOs;
using TreeApp.Application.Extensions;
using TreeApp.Application.Interfaces;
using TreeApp.Domain.Constants;
using TreeApp.Domain.Exceptions;

namespace TreeApp.Application.Services;
public class JournalService : IJournalService
{
    private readonly IJournalRepository Repository;

    public JournalService(IJournalRepository repository) => Repository = repository;

    public async Task<RangeDto<JournalInfoDto>> GetJournalRangeAsync(int skip, int take, JournalFilterDto filter)
    {
        var entries = await Repository.GetJournalEntriesAsync(skip, take, filter);
        var totalCount = await Repository.GetJournalEntriesCountAsync(filter);
        var items = entries.Select(e => e.ToInfoDto()).ToList();

        return new RangeDto<JournalInfoDto>(skip, totalCount, items);
    }

    public async Task<JournalDto> GetJournalEntryAsync(Guid id)
    {
        var entry = await Repository.GetJournalEntryByIdAsync(id)
            ?? throw new SecureException(ErrorMessages.JournalEntryNotFound);

        return entry.ToDto();
    }
}