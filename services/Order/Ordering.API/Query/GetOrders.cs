using MediatR;
using Microsoft.EntityFrameworkCore;
using Ordering.API.Data;
using Ordering.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Ordering.API.Query
{
    public class GetOrders
    {
        public class Query : IRequest<IEnumerable<Order>> { }
        public class QueryHandler : IRequestHandler<Query, IEnumerable<Order>>
        {
            private readonly OrderContext _db;
            public QueryHandler(OrderContext db)
            {
                _db = db;
            }            
            public async Task<IEnumerable<Order>> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _db.orders.ToListAsync(cancellationToken);
            }
        }
    }
}
