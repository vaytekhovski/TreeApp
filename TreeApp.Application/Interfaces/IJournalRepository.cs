using TreeApp.Application.DTOs;
using TreeApp.Domain.Entities;

namespace TreeApp.Application.Interfaces;
public interface IJournalRepository
{
    Task<JournalEntry?> GetJournalEntryByIdAsync(Guid id);
    Task<List<JournalEntry>> GetJournalEntriesAsync(int skip, int take, JournalFilterDto filter);
    Task<int> GetJournalEntriesCountAsync(JournalFilterDto filter);
    Task AddJournalEntryAsync(JournalEntry entry);
    Task SaveChangesAsync();
}