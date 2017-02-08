using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Airport.Commands;
using Airport.Queries;
using CQRS.Interfaces;
using CQRS.TransportChanells;
using Microsoft.AspNetCore.Mvc;

namespace Airport.Controllers
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
		public async Task<HttpResponseMessage> TestQueryAsync()
		{
			if (this.HttpContext.WebSockets.IsWebSocketRequest)
			{
				var socket = await this.HttpContext.WebSockets.AcceptWebSocketAsync();
				this.Storage.Subscribe(typeof(TestQuery), socket.ToTransportChanell());
			}

			return new HttpResponseMessage(System.Net.HttpStatusCode.SwitchingProtocols);
		}

		[HttpPost]
		[Route("command/update")]
		public void Update(UpdateTest command)
		{
			this.Manager.Execute(command);
		}
    }
}
