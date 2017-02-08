using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Airport.Queries;
using CQRS.Services;

namespace Airport.CQRS
{
	public class AirportQueryStorage : QueryStorage
	{
		public AirportQueryStorage(IServiceProvider serviceProvider) 
			: base(serviceProvider)
		{
		}

		protected override void CreateQueryHandlers()
		{
			this.QueryHandlers.Add(typeof(TestQuery), typeof(TestQueryHandler));
		}
	}
}
