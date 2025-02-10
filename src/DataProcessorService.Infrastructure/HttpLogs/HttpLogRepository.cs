using DataProcessorService.Domain.HttpLogs;
using DataProcessorService.Infrastructure.EntityFrameworkCore;

namespace DataProcessorService.Infrastructure.HttpLogs;

public class HttpLogRepository : IHttpLogRepository
{
    private readonly DataProcessorDbContext _context;

    public HttpLogRepository(DataProcessorDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(HttpLog log)
    {
        await _context.HttpLogs.AddAsync(log);
        await _context.SaveChangesAsync();
    }
}
