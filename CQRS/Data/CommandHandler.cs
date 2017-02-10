using System;
using System.Collections.Generic;
using System.Text;
using CQRS.Interfaces;

namespace CQRS.Data
{
	public abstract class CommandHandler<TCommand> : ICommandHandler<TCommand>
		where TCommand : ICommand
	{
		public virtual void Dispose()
		{

		}

		public abstract void Handle(TCommand command);

		public void Handle(ICommand command)
		{
			this.Handle((TCommand)command);
		}
	}
}
