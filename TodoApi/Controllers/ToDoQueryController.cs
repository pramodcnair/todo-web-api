using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using TodoApi.Data;
using TodoApi.Queries;

namespace TodoApi.Controllers
{
    //  [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TodoQueryController : ControllerBase
    {
        private readonly TodoContext todoContext;
        public TodoQueryController(TodoContext _todoContext)
        {
            todoContext = _todoContext;
        }
        [HttpGet]
        public ActionResult<IEnumerable<Todo>> Get()
        {
            return Ok(todoContext.TodoList.Where(t => t.IsActive));
        }

        [HttpGet("{id}")]
        public ActionResult<Todo> Get(int id)
        {
            return Ok(todoContext.TodoList.Where(t => t.IsActive && t.Id == id).SingleOrDefault());
        }
    }
}