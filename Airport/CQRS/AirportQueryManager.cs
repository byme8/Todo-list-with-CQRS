using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Airport.Commands;
using Airport.Queries;
using CQRS.Interfaces;
using CQRS.Services;

namespace Airport.CQRS
{
	public class AirportQueryManager : QueryManager
	{
		public AirportQueryManager(IServiceProvider serviceProvider, IQueryStorage storage) 
			: base(serviceProvider, storage)
		{
		}

		protected override void CreateCommandHandlers()
		{
			this.CommandHandlers.Add(typeof(UpdateTest), typeof(UpdateTestHandler));
		}

		protected override void CreateLinks()
		{
			this.LinkQueryToCommand.Add(typeof(UpdateTest), new List<Type> { typeof(TestQuery) });
		}
	}
}
