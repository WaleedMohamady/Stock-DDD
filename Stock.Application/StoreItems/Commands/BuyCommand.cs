using MediatR;
using Microsoft.EntityFrameworkCore;
using Stock.Application.Interfaces;
using Stock.Domain.Entities;

namespace Stock.Application.StoreItems.Commands;
public class BuyCommand : IRequest<int?>
{
    public Guid StoreId { get; set; }
    public Guid ItemId { get; set; }
    public int Quantity { get; set; }
    public class Handler(IStockDbContext context) : IRequestHandler<BuyCommand, int?>
    {
        private readonly IStockDbContext _context = context;
        public async Task<int?> Handle(BuyCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var storeItem = await _context.StoreItems
                    .FirstOrDefaultAsync(storeItem => storeItem.StoreId == request.StoreId && storeItem.ItemId == request.ItemId, cancellationToken);

                if (storeItem is null)
                {
                    var newStoreItem = new StoreItem
                    {
                        Id = Guid.NewGuid(),
                        StoreId = request.StoreId,
                        ItemId = request.ItemId,
                        Quantity = request.Quantity,
                    };

                    await _context.StoreItems.AddAsync(newStoreItem, cancellationToken);
                    await _context.SaveChangesAsync(cancellationToken);
                    return newStoreItem.Quantity;
                }
                else
                {
                    storeItem.Quantity += request.Quantity;
                    await _context.SaveChangesAsync(cancellationToken);
                    return storeItem.Quantity;
                }
            }
            catch
            {
                return null;
            }
        }
    }
}
