using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using TodoApi.Queries;

namespace TodoApi.Data
{

    public class TodoContext : DbContext
    {
        public TodoContext(DbContextOptions<TodoContext> options)
            : base(options)
        { }

        public DbSet<Todo> TodoList { get; set; }


    }

}
