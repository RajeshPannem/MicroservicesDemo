using MediatR;
using Ordering.API.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Ordering.API.Command
{
    public class DeleteOrder
    {
        public class Command : IRequest
        {
            public int Id { get; set; }
        }
        public class CommandHandler : IRequestHandler<Command, Unit>
        {
            private readonly OrderContext _db;
            public CommandHandler(OrderContext db)
            {
                _db = db;
            }
            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var order = await _db.orders.FindAsync(request.Id);
                if (order == null) return Unit.Value;
                _db.orders.Remove(order);
                await _db.SaveChangesAsync(cancellationToken);
                return Unit.Value;
            }
        }
    }
}
