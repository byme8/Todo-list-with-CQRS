using System;
using System.Collections.Generic;
using System.Text;

namespace CQRS.Interfaces
{
    public interface IQueryStorage
    {
		void Subscribe(Type queryType, ITransportChanell transportChanell);
		void Refresh(IEnumerable<Type> queryTypes);
    }
}
