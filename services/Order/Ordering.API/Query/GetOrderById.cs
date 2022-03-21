using MediatR;
using Ordering.API.Data;
using Ordering.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Ordering.API.Query
{
    public class GetOrderById
    {
        public class Query:IRequest<Order>
        {
            public int Id { get; set; }
        }
        public class QueryHandler : IRequestHandler<Query, Order>
        {
            private readonly OrderContext _db;
            public QueryHandler(OrderContext db)
            {
                _db = db;
            }
            public async Task<Order> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _db.orders.FindAsync(request.Id);
            }
        }
    }
}
