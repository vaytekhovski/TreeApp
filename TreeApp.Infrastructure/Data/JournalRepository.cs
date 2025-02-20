using Microsoft.EntityFrameworkCore;
using TreeApp.Application.DTOs;
using TreeApp.Application.Interfaces;
using TreeApp.Domain.Entities;

namespace TreeApp.Infrastructure.Data;
public class JournalRepository : IJournalRepository
{
    private readonly ApplicationDbContext Context;

    public JournalRepository(ApplicationDbContext context) => Context = context;

    public async Task<JournalEntry?> GetJournalEntryByIdAsync(Guid id)
        => await Context.JournalEntries.FindAsync(id);

    public async Task<List<JournalEntry>> GetJournalEntriesAsync(int skip, int take, JournalFilterDto filter)
    {
        var query = ApplyFilter(Context.JournalEntries.AsQueryable(), filter);
        return await query
            .OrderByDescending(j => j.CreatedAt)
            .Skip(skip)
            .Take(take)
            .ToListAsync();
    }

    public async Task<int> GetJournalEntriesCountAsync(JournalFilterDto filter)
    {
        var query = ApplyFilter(Context.JournalEntries.AsQueryable(), filter);
        return await query.CountAsync();
    }

    public Task AddJournalEntryAsync(JournalEntry entry) => Task.FromResult(Context.JournalEntries.Add(entry));
    public Task SaveChangesAsync() => Context.SaveChangesAsync();

    private static IQueryable<JournalEntry> ApplyFilter(IQueryable<JournalEntry> query, JournalFilterDto filter)
    {
        if (filter.From.HasValue)
            query = query.Where(j => j.CreatedAt >= filter.From.Value);
        if (filter.To.HasValue)
            query = query.Where(j => j.CreatedAt <= filter.To.Value);
        if (!string.IsNullOrEmpty(filter.Search))
            query = query.Where(j => j.StackTrace.Contains(filter.Search));
        return query;
    }
}