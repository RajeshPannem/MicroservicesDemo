using Catalog.API.Data;
using Catalog.API.Entities;
using MediatR;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Catalog.API.Command
{
    public class DeleteProduct
    {
        public class Command : IRequest
        {
            public string Id { get; set; }
        }
        public class CommandHandler : IRequestHandler<Command, Unit>
        {
            private readonly ICatalogContext _db;
            public CommandHandler(ICatalogContext db)
            {
                _db = db;
            }
            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {                
                FilterDefinition<Products> filter = Builders<Products>.Filter.Eq(p => p.Id, request.Id);
                DeleteResult deleteResult = await _db.products.DeleteOneAsync(filter,cancellationToken);
                if (deleteResult.DeletedCount > 0 && deleteResult.IsAcknowledged) return Unit.Value;               
                return Unit.Value;
                
            }
        }
    }
}
