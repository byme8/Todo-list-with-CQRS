using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Todo.Services;
using CQRS.Interfaces;

namespace Todo.Commands
{
	public class UpdateTest : ICommand
	{
		public string NewText
		{
			get;
			set;
		}
	}

	public class UpdateTestHandler : ICommandHandler
	{
		private TestService testService;

		public void Dispose()
		{

		}

		public UpdateTestHandler(TestService service)
		{
			this.testService = service;
		}

		public void Handle(ICommand command)
		{
			var updateCommand = command as UpdateTest;
			this.testService.Text = updateCommand.NewText;
		}
	}
}
