using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TodoApi.Commands;
using TodoApi.Data;
using TodoApi.Queries;

namespace TodoApi.Handler
{
    public class TodoHandler : IRequestHandler<TodoAddCommand, Todo>, IRequestHandler<TodoUpdateCommand, Todo>, IRequestHandler<TodoDeleteCommand, Todo>
    {
        private readonly TodoContext todoContext;
        public TodoHandler(TodoContext _todoContext)
        {
            todoContext = _todoContext;
        }
        public async Task<Todo> Handle(TodoAddCommand request, CancellationToken cancellationToken)
        {
            var todo = new Todo
            {
                CreatedOn = DateTime.UtcNow,
                CreatedBy = request.CreatedBy,
                Description = request.Description,
                UpdatedBy = request.CreatedBy,
                UpdatedOn = DateTime.UtcNow,
                IsActive = true
            };
            todoContext.Add(todo);
            await todoContext.SaveChangesAsync();
            return todo;
        }
        public async Task<Todo> Handle(TodoUpdateCommand request, CancellationToken cancellationToken)
        {
            var todo = await todoContext.TodoList.FindAsync(request.Id);
            todo.Description = request.Description;
            todo.UpdatedBy = request.UpdatedBy;
            todo.UpdatedOn = DateTime.UtcNow;
            await todoContext.SaveChangesAsync();
            return todo;

        }

        public async Task<Todo> Handle(TodoDeleteCommand request, CancellationToken cancellationToken)
        {
            var todo = await todoContext.TodoList.FindAsync(request.Id);
            todo.UpdatedBy = request.UpdatedBy;
            todo.UpdatedOn = DateTime.UtcNow;
            todo.IsActive = false;
            await todoContext.SaveChangesAsync();
            return todo;
        }


    }
}
