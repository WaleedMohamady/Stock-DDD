using MediatR;
using Microsoft.EntityFrameworkCore;
using Stock.Application.Interfaces;

namespace Stock.Application.Items.Commands;
public class DeleteItemCommand : IRequest<string>
{
    public Guid Id { get; set; }
    public class Handler(IStockDbContext context) : IRequestHandler<DeleteItemCommand, string>
    {
        private readonly IStockDbContext _context = context;
        public async Task<string> Handle(DeleteItemCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var item = await _context.Items
                    .FirstOrDefaultAsync(item => item.Id == request.Id, cancellationToken);

                _context.Items.Remove(item);
                await _context.SaveChangesAsync(cancellationToken);
                return "Item has been deleted successfully";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
