using Fibon.Api.Framework;
using Fibon.Api.Handlers;
using Fibon.Api.Repository;
using Fibon.Messages.Events;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RawRabbit;
using RawRabbit.vNext;

namespace Fibon.Api
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
            ConfigureRabbitMq(services);
            services.AddSingleton<IRepository>(_ => new Repository.Repository());
            services.AddScoped<IEventHandler<ValueCalculatedEvent>, ValueCalculatedEventHandler>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseMvc();

            ConfigureRabbitMqStuff(app);
        }

        private void ConfigureRabbitMqStuff(IApplicationBuilder app)
        {
            var client = app.ApplicationServices.GetService<IBusClient>();
            client.SubscribeAsync<ValueCalculatedEvent>(async (msg, ctx) =>
            {
                var handler = app.ApplicationServices.GetService<IEventHandler<ValueCalculatedEvent>>();
                await handler.HandleAsync(msg);
            });
        }

        private void ConfigureRabbitMq(IServiceCollection services)
        {
            var options = new RabbitMqOptions();
            var section = Configuration.GetSection("rabbitmq");
            section.Bind(options);

            var client = BusClientFactory.CreateDefault(options);
            services.AddSingleton<IBusClient>(client);
        }
    }
}
