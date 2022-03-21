using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ordering.API.Command;
using Ordering.API.Models;
using Ordering.API.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ordering.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IMediator _mediator;
        public OrderController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet]
        public async Task<IEnumerable<Order>> GetOrders()
        {
            return await _mediator.Send(new GetOrders.Query());
        }
        [HttpGet("{id}")]
        public async Task<Order> GetOrder(int id)
        {
            return await _mediator.Send(new GetOrderById.Query() { Id = id });
        }
        [HttpPost]
        public async Task<ActionResult> CreateNewOrder([FromBody] AddNewOrder.Command command)
        {
            var CreatedOrderId = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetOrders), new { id = CreatedOrderId }, null);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteOrder(int id)
        {
            await _mediator.Send(new DeleteOrder.Command { Id = id });
            return NoContent();
        }
    }
}
