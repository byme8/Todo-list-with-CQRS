using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Todo.Commands;
using Todo.Queries;
using CQRS.Interfaces;
using CQRS.Services;

namespace Todo.CQRS
{
	public class TodoQueryManager : QueryManager
	{
		public TodoQueryManager(IServiceProvider serviceProvider, IQueryStorage storage) 
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
