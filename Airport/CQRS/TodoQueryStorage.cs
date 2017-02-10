using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Todo.Queries;
using CQRS.Services;

namespace Todo.CQRS
{
	public class TodoQueryStorage : QueryStorage
	{
		public TodoQueryStorage(IServiceProvider serviceProvider) 
			: base(serviceProvider)
		{
		}

		protected override void CreateQueryHandlers()
		{
			this.QueryHandlers.Add(typeof(AllTodoTasksQuery), typeof(AllTodoTasksQueryHandler));
		}
	}
}
