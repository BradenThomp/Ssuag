using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Comment_Microservice.Aggregates.Repository;
using Comment_Microservice.Commands.Handlers;
using Comment_Microservice.Queries;
using EventStore.ClientAPI;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Comment_Microservice
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            var eventStoreConnection = EventStoreConnection.Create(ConnectionSettings.Default, new IPEndPoint(IPAddress.Loopback, 1113));

            eventStoreConnection.ConnectAsync();

            //injects the connection into an AggregateRepository
            var repository = new AggregateRepository(eventStoreConnection);

            CommentCommandHandler[] commands = { new CommentCommandHandler(repository) };

            //inject AggregateRepository into a CommentHandlers(event handler) then map all comment commands
            var commandHandlerMap = new CommandHandlerMap(commands);

            //inject mapped commands into dispatcher
            services.AddSingleton<Dispatcher>(new Dispatcher(commandHandlerMap));

            services.AddSingleton<RavenDBConnection>(new RavenDBConnection("CommentMicroservice"));

            services.AddTransient<RavenDBRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
