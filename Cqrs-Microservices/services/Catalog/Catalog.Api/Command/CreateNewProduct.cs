using Catalog.API.Data;
using Catalog.API.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Catalog.API.Command
{
    public class CreateNewProduct
    {
        public class Command : IRequest<string>
        {
            public string Name { get; set; }
            public string Category { get; set; }
            public string Summary { get; set; }
            public string Description { get; set; }
            public string ImageFile { get; set; }
            public decimal Price { get; set; }
        }
        public class CommandHandler : IRequestHandler<Command, string>
        {
            private readonly ICatalogContext _db;
            public CommandHandler(ICatalogContext db)
            {
                _db = db;
            }
            public async Task<string> Handle(Command request, CancellationToken cancellationToken)
            {
                var entity = new Products
                {
                    Name = request.Name,
                    Category = request.Category,
                    Summary = request.Summary,
                    Description = request.Description,
                    ImageFile = request.ImageFile,
                    Price = request.Price
                };
                await _db.products.InsertOneAsync(entity);
                return entity.Id;
            }
        }
    }
}
