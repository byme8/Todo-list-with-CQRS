using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Airport.Commands;
using Airport.CQRS;
using Airport.Queries;
using Airport.Services;
using CQRS;
using CQRS.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Airport
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddMvc();
			services.AddSingleton<IQueryManager, AirportQueryManager>();
			services.AddSingleton<IQueryStorage, AirportQueryStorage>();
			services.AddSingleton<UpdateTestHandler>();
			services.AddSingleton<TestQueryHandler>();
			services.AddSingleton<TestService>();
			//services.AddCQRS();
		}

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
			app.UseWebSockets( new WebSocketOptions { KeepAliveInterval = TimeSpan.FromSeconds(30), ReceiveBufferSize = 8192 });
            app.UseMvc();
        }
    }
}
