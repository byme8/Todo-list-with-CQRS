using System;
using System.Collections.Generic;
using System.Text;
using CQRS.Interfaces;

namespace CQRS.Services
{
	public abstract class QueryManager : IQueryManager
	{
		protected Dictionary<Type, Type> CommandHandlers = new Dictionary<Type, Type>();
		protected Dictionary<Type, List<Type>> LinkQueryToCommand = new Dictionary<Type, List<Type>>();

		private IServiceProvider serviceProvider;
		private IQueryStorage storage;

		public QueryManager(IServiceProvider serviceProvider, IQueryStorage storage)
		{
			this.serviceProvider = serviceProvider;
			this.storage = storage;

			this.CreateLinks();
			this.CreateCommandHandlers();
		}

		protected abstract void CreateLinks();
		protected abstract void CreateCommandHandlers();

		public void Execute(ICommand command)
		{
			var commandType = command.GetType();
			this.CommandHandlers.TryGetValue(commandType, out Type commandHandlerType);

			if (commandHandlerType is null)
				throw new InvalidOperationException($"Command {commandType.Name} is not supported.");

			using (var commadHandler = this.serviceProvider.GetService(commandHandlerType) as ICommandHandler)
				commadHandler.Handle(command);

			this.LinkQueryToCommand.TryGetValue(commandType, out List<Type> queries);
			if (queries is null)
				return;

			this.storage.Refresh(queries);
		}
	}
}
