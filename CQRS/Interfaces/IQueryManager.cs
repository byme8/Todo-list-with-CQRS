using System;
using System.Collections.Generic;
using System.Text;

namespace CQRS.Interfaces
{
    public interface IQueryManager
    {
		void Execute(ICommand command);
    }
}
