using MediatR;
using Ordering.API.Data;
using Ordering.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Ordering.API.Command
{
    public class AddNewOrder
    {
        public class Command : IRequest<int>
        {
            public string OrderName { get; set; }
            public string price { get; set; }
            public string Description { get; set; }
        }
        public class CommandHandler : IRequestHandler<Command, int>
        {
            private readonly OrderContext _db;
            public CommandHandler(OrderContext db)
            {
                _db = db;
            }
            public async Task<int> Handle(Command request, CancellationToken cancellationToken)
            {
                var entity = new Order()
                {
                    OrderName = request.OrderName,
                    Price = request.price,
                    Description = request.Description
                };
                await _db.orders.AddAsync(entity,cancellationToken);
                await _db.SaveChangesAsync(cancellationToken);
                return entity.Id;
            }
        }
    }
}
