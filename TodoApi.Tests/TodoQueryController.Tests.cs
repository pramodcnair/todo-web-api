using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using TodoApi.Controllers;
using TodoApi.Queries;
using TodoApi.Services;

namespace Tests
{
    public class TodoQueryControllerTests
    {
        private Mock<ITodoRepositoryService> mockTodoRepositoryService = new Mock<ITodoRepositoryService>();

        [Test]
        public void GetAllTodoControllerTestAsync()
        {
            mockTodoRepositoryService.Setup(x => x.GetAllItems()).Returns(GetAllTodos());
            var controller = new TodoQueryController(mockTodoRepositoryService.Object);
            var response = controller.Get().Result as OkObjectResult;
            Assert.IsInstanceOf<IEnumerable<Todo>>(response.Value);
        }

        [Test]
        public void GetTodoByIdControllerTestAsync()
        {
            mockTodoRepositoryService.Setup(x => x.GetItemById(It.IsAny<int>())).Returns(GetTodo());
            var controller = new TodoQueryController(mockTodoRepositoryService.Object);
            var response = controller.Get(1).Result as OkObjectResult;
            Assert.IsInstanceOf<Todo>(response.Value);
        }

        private IEnumerable<Todo> GetAllTodos()
        {
            var todoList = new List<Todo> {
                 new Todo { Description = "first todo" },
                 new Todo { Description = "second todo" }
               };
            return todoList;

        }
        private Todo GetTodo()
        {
            return new Todo { Description = "todo" };

        }
    }
}
