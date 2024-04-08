using MediatR;
using Microsoft.EntityFrameworkCore;
using Stock.Application.Interfaces;

namespace Stock.Application.Stores.Commands;
public class EditStoreCommand : IRequest<string>
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public class Handler(IStockDbContext context) : IRequestHandler<EditStoreCommand, string>
    {
        private readonly IStockDbContext _context = context;
        public async Task<string> Handle(EditStoreCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var store = await _context.Stores
                    .FirstOrDefaultAsync(store => store.Id == request.Id, cancellationToken);

                store.Name = request.Name;

                await _context.SaveChangesAsync(cancellationToken);
                return "Store has been updated successfully";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
