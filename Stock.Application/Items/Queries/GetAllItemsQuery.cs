using MediatR;
using Microsoft.EntityFrameworkCore;
using Stock.Application.Interfaces;
using Stock.Domain.Entities;

namespace Stock.Application.Items.Queries;
public class GetAllItemsQuery : IRequest<List<Item>>
{
    public class Handler(IStockDbContext context) : IRequestHandler<GetAllItemsQuery, List<Item>>
    {
        private readonly IStockDbContext _context = context;
        public async Task<List<Item>> Handle(GetAllItemsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var items = await _context.Items
                    .Select(item => new Item
                    {
                        Id = item.Id,
                        Name = item.Name,
                    })
                    .ToListAsync(cancellationToken);

                return items;
            }
            catch
            {
                return [];
            }
        }
    }
}
