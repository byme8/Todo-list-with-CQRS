using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Todo.Services;
using CQRS.Interfaces;
using CQRS.Data;

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

	public class UpdateTestHandler : CommandHandler
	{
		private TestService testService;

		public UpdateTestHandler(TestService service)
		{
			this.testService = service;
		}

		public override void Handle(ICommand command)
		{
			var updateCommand = command as UpdateTest;
			this.testService.Text = updateCommand.NewText;
		}
	}
}
