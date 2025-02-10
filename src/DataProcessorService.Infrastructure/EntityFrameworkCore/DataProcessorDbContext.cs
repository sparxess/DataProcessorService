using DataProcessorService.Domain.Authorization;
using DataProcessorService.Domain.Data;
using DataProcessorService.Domain.HttpLogs;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DataProcessorService.Infrastructure.EntityFrameworkCore;

public class DataProcessorDbContext : IdentityDbContext<User>
{
    public DataProcessorDbContext(DbContextOptions<DataProcessorDbContext> options)
        : base(options)
    {
    }
    public DbSet<DataItem> DataItems { get; set; }
    public DbSet<HttpLog> HttpLogs { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<DataItem>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Code);
            entity.Property(e => e.Value).HasMaxLength(255);
        });

        modelBuilder.Entity<HttpLog>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Method).HasMaxLength(10);
            entity.Property(e => e.Path).HasMaxLength(255);
            entity.Property(e => e.RequestBody);
            entity.Property(e => e.ResponseBody);
            entity.Property(e => e.Time);
        });
    }
}