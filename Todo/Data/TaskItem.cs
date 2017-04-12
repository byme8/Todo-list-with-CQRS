using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Todo.Data
{
	public class TodoTask
	{
		public bool Finished { get; set; }
		public Guid? Key { get; set; }
		public string Text { get; set; }
	}
}
