using MediatR;
using Newtonsoft.Json;
using TodoApi.Queries;

namespace TodoApi.Commands
{
    public class TodoDeleteCommand :IRequest<Todo>
    {
        public int Id { get; set; }

        [JsonIgnore]
        public string UpdatedBy { get; set; }
    }
}
