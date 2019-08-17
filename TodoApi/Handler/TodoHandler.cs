using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TodoApi.Commands;
using TodoApi.Data;
using TodoApi.Queries;
using TodoApi.Services;

namespace TodoApi.Handler
{
    public class TodoHandler : IRequestHandler<TodoAddCommand, Todo>, IRequestHandler<TodoUpdateCommand, Todo>, IRequestHandler<TodoDeleteCommand, Todo>
    {
        private readonly ITodoRepositoryService todoRepositoryService;

        public TodoHandler(ITodoRepositoryService _todoRepositoryService)
        {
            todoRepositoryService = _todoRepositoryService;
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
            return await todoRepositoryService.SaveItemAsync(todo);
        }

        public async Task<Todo> Handle(TodoUpdateCommand request, CancellationToken cancellationToken)
        {
            var todo = new Todo
            {
                Id = request.Id,
                Description = request.Description,
                UpdatedBy = request.UpdatedBy
            };
            return await todoRepositoryService.UpdateItemAsync(todo);
        }

        public async Task<Todo> Handle(TodoDeleteCommand request, CancellationToken cancellationToken)
        {
            var todo = new Todo
            {
                Id = request.Id,
                UpdatedBy = request.UpdatedBy,
            };
            return await todoRepositoryService.DeleteItemAsync(todo);
        }
    }
}
