using Moq;
using NUnit.Framework;
using System.Threading;
using System.Threading.Tasks;
using TodoApi.Commands;
using TodoApi.Handler;
using TodoApi.Queries;
using TodoApi.Services;

namespace Tests
{
    public class TodoHandlerTests
    {
        private Mock<ITodoRepositoryService> mockTodoRepositoryService = new Mock<ITodoRepositoryService>();

        [Test]
        public async Task AddCommandHandlerTestAsync()
        {
            mockTodoRepositoryService.Setup(x => x.SaveItemAsync(It.IsAny<Todo>())).Returns(Task.FromResult(GetTodo()));
            TodoHandler todoHandler = new TodoHandler(mockTodoRepositoryService.Object);
            var todoAddCommand = new TodoAddCommand();
            var result = await todoHandler.Handle(todoAddCommand, new CancellationToken());
            Assert.AreEqual("todo", result.Description);

        }

        [Test]
        public async Task UpdateCommandHandlerTestAsync()
        {
            mockTodoRepositoryService.Setup(x => x.UpdateItemAsync(It.IsAny<Todo>())).Returns(Task.FromResult(GetTodo()));
            TodoHandler todoHandler = new TodoHandler(mockTodoRepositoryService.Object);
            var todoUpdateCommand = new TodoUpdateCommand();
            var result = await todoHandler.Handle(todoUpdateCommand, new CancellationToken());
            Assert.AreEqual("todo", result.Description);

        }

        [Test]
        public async Task DeleteCommandHandlerTestAsync()
        {
            mockTodoRepositoryService.Setup(x => x.DeleteItemAsync(It.IsAny<Todo>())).Returns(Task.FromResult(GetTodo()));
            TodoHandler todoHandler = new TodoHandler(mockTodoRepositoryService.Object);
            var todoDeleteCommand = new TodoDeleteCommand();
            var result = await todoHandler.Handle(todoDeleteCommand, new CancellationToken());
            Assert.AreEqual("todo", result.Description);

        }
        private Todo GetTodo()
        {
            return new Todo { Description = "todo" };

        }
    }
}
