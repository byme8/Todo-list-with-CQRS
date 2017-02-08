using System;
using System.Collections.Generic;
using System.Text;
using CQRS.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace CQRS
{
    public static class ServicesRegisters
    {
		public static void AddCQRS(this IServiceCollection services, IQueryStorage storage, IQueryManager manager)
		{
			services.AddSingleton(storage);
			services.AddSingleton(manager);
		}
    }
}
