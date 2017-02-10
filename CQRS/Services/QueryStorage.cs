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
			this.Cleanup(this.QueryHandlers.Keys);
		}

		protected abstract void CreateQueryHandlers();

		public void Cleanup(IEnumerable<Type> queryTypes)
		{
			foreach (var queryType in queryTypes)
				this.queries[queryType] = null;
		}

		private void Refresh(Type queryType)
		{
			this.QueryHandlers.TryGetValue(queryType, out Type queryHandlerType);

			if (queryHandlerType is null)
				throw new InvalidOperationException($"Query {queryType.Name} is not supported.");

			using (var queryHandler = this.serviceProvider.GetService(queryHandlerType) as IQueryHandler<IQuery>)
				this.queries[queryType] = queryHandler.Refresh();
		}

		public IQuery Get(Type queryType)
		{
			var query = this.queries[queryType];

			if (query is null)
			{
				this.Refresh(queryType);
				query = this.queries[queryType];
			}

			return this.queries[queryType];
		}

		public TQuery Get<TQuery>()
			where TQuery : class, IQuery
		{
			return this.Get(typeof(TQuery)) as TQuery;
		}
	}
}