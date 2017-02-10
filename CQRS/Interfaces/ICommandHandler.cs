using System;
using System.Collections.Generic;
using System.Text;

namespace CQRS.Interfaces
{
    public interface ICommandHandler : IDisposable
    {
		void Handle(ICommand command);
    }

	public interface ICommandHandler<TCommand> : ICommandHandler
		where TCommand : ICommand
	{
		void Handle(TCommand command);
	}
}
