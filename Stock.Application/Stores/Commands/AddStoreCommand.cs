using MediatR;
using Stock.Application.Interfaces;
using Stock.Domain.Entities;

namespace Stock.Application.Stores.Commands;
public class AddStoreCommand : IRequest<string>
{
    public string Name { get; set; }
    public class Handler(IStockDbContext context) : IRequestHandler<AddStoreCommand, string>
    {
        private readonly IStockDbContext _context = context;
        public async Task<string> Handle(AddStoreCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var newStore = new Store
                {
                    Id = Guid.NewGuid(),
                    Name = request.Name,
                };

                await _context.Stores.AddAsync(newStore, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);
                return "Store has been saved successfully";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
