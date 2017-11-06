using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using EventFlow;
using EventFlow.Aspnetcore.Middlewares;
using EventFlow.AspNetCore.Extensions;
using EventFlow.Autofac.Extensions;
using EventFlow.EventStores.EventStore.Extensions;
using EventFlow.Extensions;
using EventFlow.MetadataProviders;
using EventFlow.RabbitMQ;
using EventFlow.MongoDB.Extensions;
using EventFlow.RabbitMQ.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Pages.Api.Models.Read;

namespace Pages.Api
{
  public class Startup
  {
    public IConfiguration Configuration { get; }

    public IContainer ApplicationContainer { get; private set; }

    public static Assembly Assembly { get; } = typeof(Startup).Assembly;

    public Startup(IHostingEnvironment env)
    {
      var builder = new ConfigurationBuilder()
        .SetBasePath(env.ContentRootPath)
        .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
        .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
        .AddEnvironmentVariables();

      Configuration = builder.Build();
    }

    // This method gets called by the runtime. Use this method to add services to the container.
    public IServiceProvider ConfigureServices(IServiceCollection services)
    {
      var eventStoreUri = new Uri("tcp://localhost:1113", UriKind.Absolute);
      var rabbmitMqUri = new Uri("amqp://localhost", UriKind.Absolute);
      var mongoUrl = "mongodb://localhost:27017";

      services.AddMvc();
      var builder = new ContainerBuilder();
      var container = EventFlowOptions.New
                                      .UseAutofacContainerBuilder(builder)
                                      .ConfigureMongoDb(mongoUrl, "pages")
                                      .PublishToRabbitMq(RabbitMqConfiguration.With(rabbmitMqUri))
                                      //.ConfigureElasticsearch(new Uri("http://localhost:9200"))
                                      //.UseElasticsearchReadModel<StoryReadModel>()
                                      .UseEventStoreEventStore(eventStoreUri)
                                      .UseInMemorySnapshotStore()
                                      .AddDefaults(Assembly)
                                      .UseMongoDbReadModel<UserReadModel>()
                                      .AddMetadataProvider<AddGuidMetadataProvider>()
                                      .AddAspNetCoreMetadataProviders();

      builder.Populate(services);
      ApplicationContainer = builder.Build();
      return new AutofacServiceProvider(ApplicationContainer);
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory, IApplicationLifetime appLifetime)
    {
      loggerFactory.AddConsole(Configuration.GetSection("Logging"));
      loggerFactory.AddDebug();
      app.UseMiddleware<CommandPublishMiddleware>();
      app.UseMvc();
      appLifetime.ApplicationStopped.Register(() => ApplicationContainer.Dispose());
    }
  }
}
