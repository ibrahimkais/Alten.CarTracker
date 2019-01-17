using Alten.CarTracker.BackEndApi.Application;
using Alten.CarTracker.BackEndApi.Application.AutoMapperProfiles;
using Alten.CarTracker.CustomersService.DataAccess;
using Alten.CarTracker.Infrastructure.Messaging;
using Alten.CarTracker.Infrastructure.ServiceDiscovery;
using AutoMapper;
using Consul;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.HealthChecks;
using Serilog;
using Swashbuckle.AspNetCore.Swagger;
using System;

namespace Alten.CarTracker.BackEndApi.Api
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
			string sqlConnectionString = Configuration.GetConnectionString("CarTrackerDbConnection");

			services.AddDbContext<EfDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString(sqlConnectionString)))
				.AddUnitOfWork<EfDbContext>();

			Mapper.Initialize(cfg => cfg.AddProfile<MappingProfile>());
			services.AddAutoMapper();

			var configSection = Configuration.GetSection("RabbitMQ");
			string host = configSection["Host"];
			string userName = configSection["UserName"];
			string password = configSection["Password"];
			services.AddTransient<IMessagePublisher>((sp) => new RabbitMQMessagePublisher(host, userName, password, "StatusReceivedEx"));

			services.Configure<ConsulConfig>(Configuration.GetSection("consulConfig"));
			services.AddSingleton<IConsulClient, ConsulClient>(p => new ConsulClient(consulConfig =>
			{
				var address = Configuration["consulConfig:address"];
				consulConfig.Address = new Uri(address);
			}));

			services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

			services.AddScoped<ICustomerService, CustomerService>();
			services.AddScoped<IVehicleService, VehicleService>();


			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new Info { Title = "BackEndApi", Version = "v1" });
			});

			services.AddHealthChecks(checks =>
			{
				checks.WithDefaultCacheDuration(TimeSpan.FromSeconds(1));
				checks.AddSqlCheck("BackEndApi", sqlConnectionString);
			});
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env, IApplicationLifetime lifetime, EfDbContext dbContext)
		{
			Log.Logger = new LoggerConfiguration()
			   .ReadFrom.Configuration(Configuration)
			   .CreateLogger();

			app.UseMvc();
			app.UseDefaultFiles();
			app.UseStaticFiles();

			// Enable middleware to serve generated Swagger as a JSON endpoint.
			app.UseSwagger();

			// Enable middleware to serve swagger-ui (HTML, JS, CSS etc.), specifying the Swagger JSON endpoint.
			app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "BackEndApi - v1"));

			// register service in Consul
			app.RegisterWithConsul(lifetime);
		}
	}
}
