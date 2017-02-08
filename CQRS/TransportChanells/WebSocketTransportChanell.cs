using System;
using System.Collections.Generic;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CQRS.Interfaces;
using Newtonsoft.Json;

namespace CQRS.TransportChanells
{
	public class WebSocketTransportChanell : ITransportChanell
	{
		private WebSocket socket;

		public WebSocketTransportChanell(WebSocket socket)
		{
			this.socket = socket;

			//Task.Run(this.ListiningAsync);
		}
		
		private async Task ListiningAsync()
		{
			var arraySegment = new ArraySegment<byte>(new byte[4]);
			var zeroZegment = new ArraySegment<byte>(new byte[0]);
			while (this.socket.State == WebSocketState.Open)
			{
				var data = await this.socket.ReceiveAsync(arraySegment, CancellationToken.None);
				await this.socket.SendAsync(zeroZegment,
					WebSocketMessageType.Text,
					false,
					CancellationToken.None);
			}
		}

		public void Send(IQuery query)
		{
			var json = JsonConvert.SerializeObject(query);
			this.socket.SendAsync(
				new ArraySegment<byte>(Encoding.UTF8.GetBytes(json)),
				WebSocketMessageType.Text,
				true,
				CancellationToken.None).Wait();
		}
	}

	public static class Extentions
	{
		public static ITransportChanell ToTransportChanell(this WebSocket socket)
		{
			return new WebSocketTransportChanell(socket);
		}
	}
}
