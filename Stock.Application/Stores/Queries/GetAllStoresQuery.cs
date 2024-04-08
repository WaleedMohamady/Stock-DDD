using MediatR;
using Microsoft.EntityFrameworkCore;
using Stock.Application.Interfaces;
using Stock.Domain.Entities;

namespace Stock.Application.Stores.Queries;
public class GetAllStoresQuery : IRequest<List<Store>>
{
    public class Handler(IStockDbContext context) : IRequestHandler<GetAllStoresQuery, List<Store>>
    {
        private readonly IStockDbContext _context = context;
        public async Task<List<Store>> Handle(GetAllStoresQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var stores = await _context.Stores
                    .Select(store => new Store
                    {
                        Id = store.Id,
                        Name = store.Name,
                    })
                    .ToListAsync(cancellationToken);

                return stores;
            }
            catch
            {
                return [];
            }
        }
    }
}
