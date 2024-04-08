using MediatR;
using Microsoft.EntityFrameworkCore;
using Stock.Application.Interfaces;

namespace Stock.Application.Stores.Commands;
public class DeleteStoreCommand : IRequest<string>
{
    public Guid Id { get; set; }
    public class Handler(IStockDbContext context) : IRequestHandler<DeleteStoreCommand, string>
    {
        private readonly IStockDbContext _context = context;
        public async Task<string> Handle(DeleteStoreCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var store = await _context.Stores
                    .FirstOrDefaultAsync(store => store.Id == request.Id, cancellationToken);

                _context.Stores.Remove(store);
                await _context.SaveChangesAsync(cancellationToken);
                return "Store has been deleted successfully";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
