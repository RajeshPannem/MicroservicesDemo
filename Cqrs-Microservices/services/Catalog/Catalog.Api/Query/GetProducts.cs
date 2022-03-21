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
    public class GetProducts
    {
        public class Query : IRequest<IEnumerable<Products>> { }
        public class QueryHandler : IRequestHandler<Query, IEnumerable<Products>>
        {
            private readonly ICatalogContext _db;
            public QueryHandler(ICatalogContext db)
            {
                _db = db;
            }
            public async Task<IEnumerable<Products>> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _db.products.Find(p=>true).ToListAsync(cancellationToken);
            }
        }
    }
}
