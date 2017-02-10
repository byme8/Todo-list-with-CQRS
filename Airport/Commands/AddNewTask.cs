using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CQRS.Data;
using CQRS.Interfaces;
using Todo.Services;

namespace Todo.Commands
{
	public class AddNewTask : ICommand
	{
		public string Message
		{
			get;
			set;
		}
	}

	public class AddNewTaskHandler : CommandHandler<AddNewTask>
	{
		private TodoService todoService;

		public AddNewTaskHandler(TodoService todoService)
		{
			this.todoService = todoService;
		}

		public override void Handle(AddNewTask command)
		{
			this.todoService.New(command.Message);
		}
	}
}
