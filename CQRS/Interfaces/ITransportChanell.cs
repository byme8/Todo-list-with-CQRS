using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Interfaces
{
    public interface ITransportChanell
	{
		void Send(IQuery query);
    }
}
