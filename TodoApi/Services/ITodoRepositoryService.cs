using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApi.Queries;

namespace TodoApi.Services
{
    public interface ITodoRepositoryService
    {
        IEnumerable<Todo> GetAllItems();
        Todo GetItemById(int id);
        Task<Todo> SaveItemAsync(Todo todoItem);
        Task<Todo> UpdateItemAsync(Todo todoItem);
        Task<Todo> DeleteItemAsync(Todo todoItem);
    }
}
