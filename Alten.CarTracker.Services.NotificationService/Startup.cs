using Alten.CarTracker.Infrastructure.Messaging;
using Alten.CarTracker.Services.NotificationService.MappingProfiles;
using Alten.CarTracker.Services.NotificationService.MessageHandler;
using Alten.CarTracker.Services.NotificationService.SignalRHubs;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

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

			services.AddSignalR();

			var configSection = Configuration.GetSection("RabbitMQ");
			string host = configSection["Host"];
			string userName = configSection["UserName"];
			string password = configSection["Password"];
			string exchange = configSection["Exchange"];

			services.AddTransient<IMessagePublisher>((sp) => new RabbitMQMessagePublisher(host, userName, password, exchange));
			services.AddTransient<IMessageHandler>((sp) => new RabbitMQMessageHandler(host, userName, password, exchange, null, null));
			services.AddSingleton<StatusReceivedMessageHandler>();

			services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env, IApplicationLifetime lifetime)
		{
			app.UseMvc();
			app.UseDefaultFiles();
			app.UseStaticFiles();

			app.UseSignalR(route =>
			{
				route.MapHub<FrontNotificationHub>("/frontNotificationHub");
			});
		}
	}
}
