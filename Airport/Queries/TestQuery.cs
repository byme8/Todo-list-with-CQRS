using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Todo.Services;
using CQRS.Interfaces;
using CQRS.Data;

namespace Todo.Queries
{
	public class TestQuery : IQuery
	{
		public string Text
		{
			get;
			set;
		}
	}

	public class TestQueryHandler : QueryHandler<TestQuery>
	{
		private TestService testService;

		public TestQueryHandler(TestService service)
		{
			this.testService = service;
		}

		public override TestQuery Refresh()
		{
			return new TestQuery
			{
				Text = this.testService.Text
			};
		}
	}
}
