using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using TodoApi.Data;
using TodoApi.Queries;
using TodoApi.Services;

namespace TodoApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TodoQueryController : ControllerBase
    {
        private readonly ITodoRepositoryService todoRepositoryService;
        public TodoQueryController(ITodoRepositoryService _todoRepositoryService)
        {
            todoRepositoryService = _todoRepositoryService;
        }
        [HttpGet]
        public ActionResult<IEnumerable<Todo>> Get()
        {
            return Ok(todoRepositoryService.GetAllItems());
        }

        [HttpGet("{id}")]
        public ActionResult<Todo> Get(int id)
        {
            return Ok(todoRepositoryService.GetItemById(id));
        }
    }
}