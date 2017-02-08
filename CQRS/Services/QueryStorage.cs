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
					this.queries[queryType] = queryHandler.Refresh();
			}
		}

		public IQuery Get(Type queryType)
		{
			return this.queries[queryType];
		}

		public TQuery Get<TQuery>()
			where TQuery : class, IQuery
		{
			return this.queries[typeof(TQuery)] as TQuery;
		}
	}
}