using Alten.CarTracker.Infrastructure.Messaging;
using Alten.CarTracker.Infrastructure.ServiceDiscovery;
using Alten.CarTracker.Services.NotificationService.MappingProfiles;
using Alten.CarTracker.Services.NotificationService.MessageHandler;
using Alten.CarTracker.Services.NotificationService.SignalRHubs;
using AutoMapper;
using Consul;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Swashbuckle.AspNetCore.Swagger;
using System;

namespace Alten.CarTracker.Services.NotificationService
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

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			Mapper.Initialize(cfg => cfg.AddProfile<MappingProfile>());
			services.AddAutoMapper();

			services.AddCors(options => options.AddPolicy("CorsPolicy",
					builder => builder.WithOrigins("http://localhost:4200", "http://localhost:12004")
					.AllowAnyMethod()
					.AllowAnyHeader()
					.AllowCredentials())
			);

			services.AddSignalR();

			var configSection = Configuration.GetSection("RabbitMQ");
			string host = configSection["Host"];
			string userName = configSection["UserName"];
			string password = configSection["Password"];
			string exchange = configSection["Exchange"];

			services.AddSingleton((sp) => new RabbitMQMessageHandler(host, userName, password, exchange, "StatusReceived", "StatusReceived"));

			// add consul
			services.Configure<ConsulConfig>(Configuration.GetSection("consulConfig"));
			services.AddSingleton<IConsulClient, ConsulClient>(p => new ConsulClient(consulConfig =>
			{
				var address = Configuration["consulConfig:address"];
				consulConfig.Address = new Uri(address);
			}));
			services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

			// Register the Swagger generator, defining one or more Swagger documents
			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new Info { Title = "Notification Service", Version = "v1" });
			});

			services.AddHealthChecks(checks => checks.WithDefaultCacheDuration(TimeSpan.FromSeconds(1)));
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env, IApplicationLifetime lifetime)
		{
			app.UseCors("CorsPolicy");
			app.UseSignalR(route =>
			{
				route.MapHub<StatusHub>("/status");
				route.MapHub<DissconnectedHub>("/disconnected");
			});

			Log.Logger = new LoggerConfiguration()
				.ReadFrom.Configuration(Configuration)
				.CreateLogger();

			app.UseMvc();
			app.UseDefaultFiles();
			app.UseStaticFiles();

			var statusHubContext = app.ApplicationServices.GetService<IHubContext<StatusHub>>();
			var dissconectedHubContext = app.ApplicationServices.GetService<IHubContext<DissconnectedHub>>();
			StatusReceivedMessageHandler statusReceivedMessageHandler = new StatusReceivedMessageHandler(Mapper.Instance, statusHubContext, dissconectedHubContext);
			var handler = app.ApplicationServices.GetService<RabbitMQMessageHandler>();
			handler.Start(statusReceivedMessageHandler);

			app.UseSwagger();

			app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Notification Service"));

			app.RegisterWithConsul(lifetime);
		}
	}
}
