using DataProcessorService.Domain.Data;
using DataProcessorService.Infrastructure.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DataProcessorService.Infrastructure.Data;

public class DataRepository : IDataRepository
{
    private readonly DataProcessorDbContext _context;

    public DataRepository(DataProcessorDbContext context)
    {
        _context = context;
    }

    public async Task SaveItemsAsync(IList<DataItem> items)
    {
        _context.DataItems.RemoveRange(_context.DataItems);
        await _context.SaveChangesAsync();

        await _context.DataItems.AddRangeAsync(items);
        await _context.SaveChangesAsync();
    }

    public async Task<IList<DataItem>> GetItemsAsync(int? code)
    {
        var query = _context.DataItems.AsQueryable();

        if (code.HasValue)
        {
            query = query.Where(d => d.Code == code.Value);
        }

        return await query.OrderBy(d => d.Id).ToListAsync();
    }
}