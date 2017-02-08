using System;
using System.Collections.Generic;
using System.Text;
using CQRS.Interfaces;

namespace CQRS.Services
{
	public abstract class QueryStorage : IQueryStorage
	{
		private IServiceProvider serviceProvider;
		protected Dictionary<Type, Type> QueryHandlers
			= new Dictionary<Type, Type>();

		private Dictionary<Type, IQuery> queries 
			= new Dictionary<Type, IQuery>();

		private Dictionary<Type, List<ITransportChanell>> transportChanells 
			= new Dictionary<Type, List<ITransportChanell>>();

		public QueryStorage(IServiceProvider serviceProvider)
		{
			this.serviceProvider = serviceProvider;

			this.CreateQueryHandlers();
			this.Refresh(this.QueryHandlers.Keys);
		}

		protected abstract void CreateQueryHandlers();

		public void Refresh(IEnumerable<Type> queryTypes)
		{
			foreach (var queryType in queryTypes)
			{
				this.QueryHandlers.TryGetValue(queryType, out Type queryHandlerType);

				if (queryHandlerType is null)
					throw new InvalidOperationException($"Query {queryType.Name} is not supported.");

				using (var queryHandler = this.serviceProvider.GetService(queryHandlerType) as IQueryHandler<IQuery>)
				{
					var query = queryHandler.Refresh();
					this.queries[queryType] = query;
					if (this.transportChanells.ContainsKey(queryType))
						foreach (var transportChanell in this.transportChanells[queryType].ToArray())
							this.TryToSend(query, transportChanell);
				}
			}
		}

		private void TryToSend(IQuery query, ITransportChanell transportChanell)
		{
			try
			{
				transportChanell.Send(query);
			}
			catch (Exception)
			{
				this.transportChanells[query.GetType()].Remove(transportChanell);
			}
		}

		public void Subscribe(Type queryType, ITransportChanell transportChanell)
		{
			if (!this.transportChanells.ContainsKey(queryType))
				this.transportChanells.Add(queryType, new List<ITransportChanell>() { transportChanell });
			else
				this.transportChanells[queryType].Add(transportChanell);

			var currentQueryValue = this.queries[queryType];
			this.TryToSend(currentQueryValue, transportChanell);
		}
	}
}