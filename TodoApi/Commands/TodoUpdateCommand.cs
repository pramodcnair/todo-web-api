using MediatR;
using Newtonsoft.Json;
using TodoApi.Queries;

namespace TodoApi.Commands
{
    public class TodoUpdateCommand : IRequest<Todo>
    {
        [JsonIgnore]
        public int Id { get; set; }
        public string Description { get; set; }

        [JsonIgnore]
        public string UpdatedBy { get; set; }
    }
}
