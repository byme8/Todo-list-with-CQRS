using System;
using System.Collections.Generic;
using System.Text;
using CQRS.Interfaces;

namespace CQRS.Data
{
	public abstract class QueryHandler<TQuery> : IQueryHandler<TQuery>
		where TQuery : IQuery
	{
		public virtual void Dispose()
		{
		}

		public abstract TQuery Refresh();
	}
}
