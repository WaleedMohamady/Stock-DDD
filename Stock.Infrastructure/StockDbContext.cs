using Microsoft.EntityFrameworkCore;
using Stock.Application.Interfaces;
using Stock.Domain.Entities;

namespace Stock.Infrastructure;
public class StockDbContext(DbContextOptions<StockDbContext> options) : DbContext(options), IStockDbContext
{
    public DbSet<Store> Stores { get; set; }
    public DbSet<Item> Items { get; set; }
    public DbSet<StoreItem> StoreItems { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(StockDbContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }
    public override int SaveChanges()
    {
        return base.SaveChanges();
    }
}
