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

	public class AddNewTaskCommandHandler : CommandHandler
	{
		private TodoService todoService;

		public AddNewTaskCommandHandler(TodoService todoService)
		{
			this.todoService = todoService;
		}

		public override void Handle(ICommand command)
		{
			var addNewTask = command as AddNewTask;
			this.todoService.New(addNewTask.Message);
		}
	}
}
