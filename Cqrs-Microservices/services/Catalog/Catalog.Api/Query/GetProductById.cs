using Catalog.API.Data;
using Catalog.API.Entities;
using MediatR;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Catalog.API.Query
{
    public class GetProductById
    {
        public class Query : IRequest<Products>{
            public string Id { get; set; }
        }
        public class QueryHandler : IRequestHandler<Query, Products>
        {
            private readonly ICatalogContext _db;
            public QueryHandler(ICatalogContext db)
            {
                _db = db;
            }
            public async Task<Products> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _db.products.Find(p => p.Id==request.Id).FirstOrDefaultAsync();
            }
        }
    }
}
