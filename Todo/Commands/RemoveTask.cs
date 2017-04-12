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

	public class RemoveTaskHandler : CommandHandler<RemoveTask>
	{
		private TodoService todoService;

		public RemoveTaskHandler(TodoService service)
		{
			this.todoService = service;
		}

		public override void Handle(RemoveTask command)
		{
			this.todoService.Remove(Guid.Parse(command.Key));
		}
	}
}
