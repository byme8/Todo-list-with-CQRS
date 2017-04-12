using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Todo.Services;
using CQRS.Interfaces;
using CQRS.Data;
using Todo.Data;

namespace Todo.Queries
{
	public class AllTodoTasksQuery : IQuery
	{
		public IEnumerable<TodoTask> Tasks
		{
			get;
			set;
		}
	}

	public class AllTodoTasksQueryHandler : QueryHandler<AllTodoTasksQuery>
	{
		private TodoService todoService;

		public AllTodoTasksQueryHandler(TodoService service)
		{
			this.todoService = service;
		}

		public override AllTodoTasksQuery Refresh()
		{
			return new AllTodoTasksQuery
			{
				Tasks = this.todoService.GetAll().ToArray()
			};
		}
	}
}
