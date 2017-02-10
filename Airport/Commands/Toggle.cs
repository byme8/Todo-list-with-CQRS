using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CQRS.Data;
using CQRS.Interfaces;
using Todo.Services;

namespace Todo.Commands
{
    public class Toggle : ICommand
    {
		public string Key
		{
			get;
			set;
		}
    }

	public class ToggleHandler : CommandHandler<Toggle>
	{
		private TodoService todoService;

		public ToggleHandler(TodoService todoService)
		{
			this.todoService = todoService;
		}

		public override void Handle(Toggle command)
		{
			this.todoService.Toggle(Guid.Parse(command.Key));
		}
	}
}
