using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApi.Data;
using TodoApi.Queries;

namespace TodoApi.Services
{
    public class TodoRepositoryService : ITodoRepositoryService
    {
        private readonly TodoContext todoContext;
        public TodoRepositoryService(TodoContext _todoContext)
        {
            todoContext = _todoContext;
        }

        public IEnumerable<Todo> GetAllItems()
        {
            return todoContext.TodoList.Where(t => t.IsActive);
        }

        public Todo GetItemById(int id)
        {
            return todoContext.TodoList.Where(t => t.IsActive && t.Id == id).SingleOrDefault();
        }

        public async Task<Todo> SaveItemAsync(Todo todo)
        {
            todoContext.Add(todo);
            await todoContext.SaveChangesAsync();
            return todo;
        }

        public async Task<Todo> UpdateItemAsync(Todo todoItem)
        {
            var todo = await todoContext.TodoList.FindAsync(todoItem.Id);
            todo.Description = todoItem.Description;
            todo.UpdatedBy = todoItem.UpdatedBy;
            todo.UpdatedOn = DateTime.UtcNow;
            await todoContext.SaveChangesAsync();
            return todo;
        }

        public async Task<Todo> DeleteItemAsync(Todo todoItem)
        {
            var todo = await todoContext.TodoList.FindAsync(todoItem.Id);
            todo.UpdatedBy = todoItem.UpdatedBy;
            todo.UpdatedOn = DateTime.UtcNow;
            todo.IsActive = false;
            await todoContext.SaveChangesAsync();
            return todo;
        }
    }
}
