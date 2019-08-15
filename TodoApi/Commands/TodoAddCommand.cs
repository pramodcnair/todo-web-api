using MediatR;
using Newtonsoft.Json;
using TodoApi.Queries;

namespace TodoApi.Commands
{
    public class TodoAddCommand : IRequest<Todo>
    {
        public string Description { get; set; }

        [JsonIgnore]
        public string CreatedBy { get; set; }

    }
}
