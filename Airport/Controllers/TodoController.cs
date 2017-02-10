using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Todo.Commands;
using Todo.Queries;
using CQRS.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Todo.Controllers
{
    [Route("api/todo")]
    public class TodoController : Controller
    {
		private IQueryStorage Storage;
		private IQueryManager Manager;

		public TodoController(IQueryStorage storage, IQueryManager manager)
		{
			this.Storage = storage;
			this.Manager = manager;
		}

		[Route("tasks")]
		public AllTodoTasksQuery GetTasks()
		{
			return this.Storage.Get<AllTodoTasksQuery>();
		}

		[HttpPost]
		[Route("remove")]
		public void Update([FromBody]RemoveTask command)
		{
			this.Manager.Execute(command);
		}

		[HttpPost]
		[Route("add")]
		public void Add([FromBody]AddNewTask command)
		{
			this.Manager.Execute(command);
		}

		[HttpPost]
		[Route("toggle")]
		public void Toggle([FromBody]Toggle command)
		{
			this.Manager.Execute(command);
		}
	}
}
