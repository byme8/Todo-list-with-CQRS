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
    [Route("api")]
    public class WebSocketController : Controller
    {
		private IQueryStorage Storage;
		private IQueryManager Manager;

		public WebSocketController(IQueryStorage storage, IQueryManager manager)
		{
			this.Storage = storage;
			this.Manager = manager;
		}

		[Route("query/test")]
		public TestQuery Test()
		{
			return this.Storage.Get<TestQuery>();
		}

		[HttpPost]
		[Route("command/update")]
		public void Update(UpdateTest command)
		{
			this.Manager.Execute(command);
		}
    }
}
