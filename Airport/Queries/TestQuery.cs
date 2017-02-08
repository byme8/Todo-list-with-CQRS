using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Airport.Services;
using CQRS.Interfaces;

namespace Airport.Queries
{
	public class TestQuery : IQuery
	{
		public string Text
		{
			get;
			set;
		}
	}

	public class TestQueryHandler : IQueryHandler<TestQuery>
	{
		private TestService testService;

		public void Dispose()
		{

		}

		public TestQueryHandler(TestService service)
		{
			this.testService = service;
		}

		public TestQuery Refresh()
		{
			return new TestQuery
			{
				Text = this.testService.Text
			};
		}
	}
}
