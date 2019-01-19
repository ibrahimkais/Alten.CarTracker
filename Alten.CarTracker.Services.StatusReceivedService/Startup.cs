using Alten.CarTracker.Infrastructure.Common.Interfaces;
using Alten.CarTracker.Infrastructure.Messaging;
using Alten.CarTracker.Services.StatusReceivedService.Application;
using Alten.CarTracker.Services.StatusReceivedService.AutoMapperProfiles;
using Alten.CarTracker.Services.StatusReceivedService.Controllers;
using Alten.CarTracker.Services.StatusReceivedService.DataAccess;
using Alten.CarTracker.Services.StatusReceivedService.MessageHandler;
using AutoMapper;
using JKang.IpcServiceFramework;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Alten.CarTracker.Services.StatusReceivedService
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
			string sqlConnectionString = Configuration["ConnectionStrings:CarStatusDbConnection"];
			services.AddDbContext<EfDbContext>
			(
				options => options.UseSqlServer
				(
					sqlConnectionString,
					x => x.UseNetTopologySuite()
				)
			);

			Mapper.Initialize(cfg => cfg.AddProfile<MappingProfile>());
			services.AddAutoMapper();

			var configSection = Configuration.GetSection("RabbitMQ");
			string host = configSection["Host"];
			string userName = configSection["UserName"];
			string password = configSection["Password"];
			string exchange = configSection["Exchange"];

			//https://medium.com/volosoft/asp-net-core-dependency-injection-best-practices-tips-tricks-c6e9c67f9d96

			services.AddSingleton(sp => StatusCheckList.Instance);


			services.AddTransient<IMessagePublisher>((sp) => new RabbitMQMessagePublisher(host, userName, password, exchange));
			services.AddSingleton((sp) => new RabbitMQMessageHandler(host, userName, password, exchange, "MinuteHasPassed", "MinuteHasPassed"));

			services.AddIpc(builder =>
			{
				builder.AddNamedPipe()
				.AddService<ICarStatusService, CarStatusService>();
			});

			services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env, IApplicationLifetime lifetime, EfDbContext dbContext, IMapper mapper)
		{
			app.UseMvc();
			app.UseDefaultFiles();
			app.UseStaticFiles();

			var messagePublisher = app.ApplicationServices.GetService<IMessagePublisher>();

			MinutHasPassedMessageHandler minutHasPassedMessageHandler = new MinutHasPassedMessageHandler(mapper, messagePublisher, dbContext);

			var handler = app.ApplicationServices.GetService<RabbitMQMessageHandler>();
			handler.Start(minutHasPassedMessageHandler);
		}
	}
}
