using Microsoft.EntityFrameworkCore;
using Stock.Domain.Entities;

namespace Stock.Application.Interfaces;
public interface IStockDbContext
{
    DbSet<Store> Stores { get; set; }
    DbSet<Item> Items { get; set; }
    DbSet<StoreItem> StoreItems { get; set; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
