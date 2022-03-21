using Catalog.API.Entities;
using Catalog.API.Query;
using Catalog.API.Command;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using System.Net;

namespace Catalog.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<ProductsController> _logger;
        public ProductsController(IMediator mediator, ILogger<ProductsController> logger)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _logger = logger?? throw new ArgumentNullException(nameof(logger));
        }
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Products>),(int)HttpStatusCode.OK)]
        public async Task<IEnumerable<Products>> GetProducts()
        {
            var prod=await _mediator.Send(new GetProducts.Query());
            return prod;
        }
        [HttpGet("{id:length(24)}",Name = "GetProducts")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerable<Products>), (int)HttpStatusCode.OK)]
        public async Task<Products> GetProductsById(string id)
        {
            var prod=await _mediator.Send(new GetProductById.Query { Id = id });
            if (prod == null)
            {
                _logger.LogError($"product with id:{id},not found");
                //return NotFound();
            }
            return prod;
        }        
        [HttpPost]
        [ProducesResponseType(typeof(IEnumerable<Products>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> AddNewProduct([FromBody] CreateNewProduct.Command product)
        {
            var createdProductId = await _mediator.Send(product);
            return CreatedAtAction(nameof(GetProducts), new { id = createdProductId }, null);
        }
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(IEnumerable<Products>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> DeleteProductById(string id)
        {
            await _mediator.Send(new DeleteProduct.Command { Id=id});
            return NoContent();
        }
    }
}
