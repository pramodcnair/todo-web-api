using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TodoApi.Commands;
using TodoApi.Queries;

namespace TodoApi.Controllers
{
    // [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TodoCommandController : ControllerBase
    {
        private readonly IMediator mediator;
        public TodoCommandController(IMediator _mediator)
        {
            mediator = _mediator;
        }
        [HttpPost("add")]
        public async Task<ActionResult<Todo>> Post([FromBody] TodoAddCommand todoAddCommand)
        {
            todoAddCommand.CreatedBy = User.Identity.Name;
            var todo = await mediator.Send(todoAddCommand);
            return Ok(todo);
        }

        [HttpPut("update")]
        public async Task<ActionResult<Todo>> Put([FromBody] TodoUpdateCommand todoUpdateCommand)
        {
            todoUpdateCommand.UpdatedBy = User.Identity.Name;
            var todo = await mediator.Send(todoUpdateCommand);
            return Ok(todo);
        }

        [HttpDelete("delete")]
        public async Task<ActionResult> Delete([FromBody] TodoDeleteCommand todoDeleteCommand)
        {
            todoDeleteCommand.UpdatedBy = User.Identity.Name;
            await mediator.Send(todoDeleteCommand);
            return Ok();
        }
    }
}