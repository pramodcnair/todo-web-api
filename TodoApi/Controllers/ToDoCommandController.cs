using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApi.Commands;
using TodoApi.Data;
using TodoApi.Queries;

namespace TodoApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ToDoCommandController : ControllerBase
    {
        private readonly IMediator mediator;
        private readonly TodoContext todoContext;
        public ToDoCommandController(IMediator _mediator, TodoContext _todoContext)
        {
            mediator = _mediator;
            todoContext = _todoContext;
        }
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return Ok(todoContext.TodoList.Where(t => t.IsActive));
        }

        [HttpPost]
        public async Task<ActionResult<Todo>> Post([FromBody] TodoAddCommand todoAddCommand)
        {
            todoAddCommand.CreatedBy = User.Identity.Name;
            var todo = await mediator.Send(todoAddCommand);
            return Ok(todo);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Todo>> Put(int id, [FromBody] TodoUpdateCommand todoUpdateCommand)
        {
            todoUpdateCommand.Id = id;
            todoUpdateCommand.UpdatedBy = User.Identity.Name;
            var todo = await mediator.Send(todoUpdateCommand);
            return Ok(todo);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var todoDeleteCommand = new TodoDeleteCommand
            {
                Id = id,
                UpdatedBy = User.Identity.Name
            };
            await mediator.Send(todoDeleteCommand);
            return Ok();
        }
    }
}