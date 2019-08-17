using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using TodoApi.Commands;
using TodoApi.Controllers;
using TodoApi.Queries;

namespace Tests
{
    public class TodoCommanderControllerTests
    {
        private ClaimsPrincipal user;
        private Mock<IMediator> mockMediator = new Mock<IMediator>();
        [SetUp]
        public void Setup()
        {
            user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
                        {
                            new Claim(ClaimTypes.Name, "dummy name"),
                            new Claim(ClaimTypes.NameIdentifier, "1"),
                            new Claim("custom-claim", "dummy claim value"),
                        }, "mock"));
        }

        [Test]
        public async Task CreateTodoControllerTestAsync()
        {
            var todoAddCommand = new TodoAddCommand();
            mockMediator.Setup(x => x.Send(It.IsAny<TodoAddCommand>(), new CancellationToken())).Returns(Task.FromResult(GetTodo()));
            var controller = new TodoCommandController(mockMediator.Object);
            controller.ControllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext() { User = user }
            };
            var actionResult = await controller.Post(todoAddCommand);
            Assert.IsInstanceOf<ActionResult<Todo>>(actionResult);
        }

        [Test]
        public async Task UpdateTodoControllerTestAsync()
        {
            var todoUpdateCommand = new TodoUpdateCommand();
            mockMediator.Setup(x => x.Send(It.IsAny<TodoUpdateCommand>(), new CancellationToken())).Returns(Task.FromResult(GetTodo()));
            var controller = new TodoCommandController(mockMediator.Object);
            controller.ControllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext() { User = user }
            };
            var actionResult = await controller.Put(todoUpdateCommand);
            Assert.IsInstanceOf<ActionResult<Todo>>(actionResult);
        }

        [Test]
        public async Task DeleteTodoControllerTestAsync()
        {
            var todoUpdateCommand = new TodoDeleteCommand();
            mockMediator.Setup(x => x.Send(It.IsAny<TodoDeleteCommand>(), new CancellationToken())).Returns(Task.FromResult(GetTodo()));
            var controller = new TodoCommandController(mockMediator.Object);
            controller.ControllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext() { User = user }
            };
            var actionResult = await controller.Delete(todoUpdateCommand);
            Assert.IsInstanceOf<ActionResult>(actionResult);
        }

        private Todo GetTodo()
        {
            return new Todo
            {
                Id = 1,
                Description = "Todo",
                CreatedOn = DateTime.UtcNow
            };
        }
    }
}