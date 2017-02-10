using System;
using System.Collections.Generic;
using System.Text;

namespace CQRS.Interfaces
{
    public interface IQueryStorage
    {
		IQuery Get(Type queryType);
		TQuery Get<TQuery>()
			where TQuery : class, IQuery;
		void Cleanup(IEnumerable<Type> queryTypes);
    }
}
