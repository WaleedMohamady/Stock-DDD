using MediatR;
using Microsoft.EntityFrameworkCore;
using Stock.Application.Interfaces;

namespace Stock.Application.StoreItems.Queries;
public class GetStoreItemQuantityQuery : IRequest<int?>
{
    public Guid StoreId { get; set; }
    public Guid ItemId { get; set; }
    public class Handler(IStockDbContext context) : IRequestHandler<GetStoreItemQuantityQuery, int?>
    {
        private readonly IStockDbContext _context = context;
        public async Task<int?> Handle(GetStoreItemQuantityQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var quantity = await _context.StoreItems
                    .Where(storeItem => storeItem.StoreId == request.StoreId && storeItem.ItemId == request.ItemId)
                    .Select(storeItem => storeItem.Quantity)
                    .FirstOrDefaultAsync(cancellationToken);

                return quantity;
            }
            catch
            {
                return null;
            }
        }
    }
}
