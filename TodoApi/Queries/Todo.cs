using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TodoApi.Queries
{
    public class Todo
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime UpdatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public bool IsActive { get; set; }
    }
}
