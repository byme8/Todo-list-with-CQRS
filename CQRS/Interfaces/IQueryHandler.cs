using System;
using System.Collections.Generic;
using System.Text;

namespace CQRS.Interfaces
{
    public interface IQueryHandler<out TQuery> : IDisposable
		where TQuery : IQuery
    {
		TQuery Refresh();
    }
}
