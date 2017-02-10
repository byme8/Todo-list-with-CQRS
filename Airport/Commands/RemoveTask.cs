using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Todo.Services;
using CQRS.Interfaces;
using CQRS.Data;

namespace Todo.Commands
{
	public class RemoveTask : ICommand
	{
		public string Key
		{
			get;
			set;
		}
	}

	public class RemoveTaskHandler : CommandHandler
	{
		private TodoService todoService;

		public RemoveTaskHandler(TodoService service)
		{
			this.todoService = service;
		}

		public override void Handle(ICommand command)
		{
			var removeCommand = command as RemoveTask;
			this.todoService.Remove(Guid.Parse(removeCommand.Key));
		}
	}
}
