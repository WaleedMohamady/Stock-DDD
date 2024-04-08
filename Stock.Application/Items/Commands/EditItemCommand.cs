using MediatR;
using Microsoft.EntityFrameworkCore;
using Stock.Application.Interfaces;

namespace Stock.Application.Items.Commands;
public class EditItemCommand : IRequest<string>
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public class Handler(IStockDbContext context) : IRequestHandler<EditItemCommand, string>
    {
        private readonly IStockDbContext _context = context;
        public async Task<string> Handle(EditItemCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var item = await _context.Items
                    .FirstOrDefaultAsync(item => item.Id == request.Id, cancellationToken);

                item.Name = request.Name;

                await _context.SaveChangesAsync(cancellationToken);
                return "Item has been updated successfully";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
