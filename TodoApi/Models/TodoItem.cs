using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TodoApi.Models
{
    public class TodoItem
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public bool IsComplete { get; set; }
		public string Secret { get; set; }
        public virtual Classification Classification { get; set; }
    }

    public class TodoItemDTO
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public bool IsComplete { get; set; }
    }

    public class Classification
	{
		public long Id { get; set; }
		public string Type { get; set; }
		
	}

}
