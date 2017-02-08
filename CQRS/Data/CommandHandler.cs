using System;
using System.Collections.Generic;
using System.Text;
using CQRS.Interfaces;

namespace CQRS.Data
{
	public abstract class CommandHandler : ICommandHandler
	{
		public virtual void Dispose()
		{

		}

		public abstract void Handle(ICommand command);
	}
}
