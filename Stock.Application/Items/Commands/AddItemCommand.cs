using MediatR;
using Stock.Application.Interfaces;
using Stock.Domain.Entities;

namespace Stock.Application.Items.Commands;
public class AddItemCommand : IRequest<string>
{
    public string Name { get; set; }
    public class Handler(IStockDbContext context) : IRequestHandler<AddItemCommand, string>
    {
        private readonly IStockDbContext _context = context;
        public async Task<string> Handle(AddItemCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var newItem = new Item
                {
                    Id = Guid.NewGuid(),
                    Name = request.Name,
                };

                await _context.Items.AddAsync(newItem, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);
                return "Item has been saved successfully";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
