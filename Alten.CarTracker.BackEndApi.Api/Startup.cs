﻿using Alten.CarTracker.BackEndApi.Application;
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
			string sqlConnectionString = Configuration["ConnectionStrings:CarTrackerDbConnection"];

			services.AddDbContext<EfDbContext>(options => options.UseSqlServer(sqlConnectionString))
				.AddUnitOfWork<EfDbContext>();

			services.AddCors(options =>
			{
				options.AddPolicy("CorsPolicy",
					builder => builder.AllowAnyOrigin()
					.AllowAnyMethod()
					.AllowAnyHeader()
					.AllowCredentials());
			});

			Mapper.Initialize(cfg => cfg.AddProfile<MappingProfile>());
			services.AddAutoMapper();

			var configSection = Configuration.GetSection("RabbitMQ");
			string host = configSection["Host"];
			string userName = configSection["UserName"];
			string password = configSection["Password"];
			string exchange = configSection["Exchange"];

			services.AddScoped<ICustomerService, CustomerService>();
			services.AddScoped<IVehicleService, VehicleService>();
			services.AddTransient<IMessagePublisher>((sp) => new RabbitMQMessagePublisher(host, userName, password, exchange));

			// add consul
			services.Configure<ConsulConfig>(Configuration.GetSection("consulConfig"));
			services.AddSingleton<IConsulClient, ConsulClient>(p => new ConsulClient(consulConfig =>
			{
				var address = Configuration["consulConfig:address"];
				consulConfig.Address = new Uri(address);
			}));
			services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
			// Register the Swagger generator, defining one or more Swagger documents
			services.AddSwaggerGen(c => c.SwaggerDoc("v1", new Info { Title = "BackEndApi", Version = "v1" }));

			services.AddHealthChecks(checks =>
			{
				checks.WithDefaultCacheDuration(TimeSpan.FromSeconds(1));
				checks.AddSqlCheck("CarTrackerDbConnection", Configuration.GetConnectionString("CarTrackerDbConnection"));
			});
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env, IApplicationLifetime lifetime)
		{
			Log.Logger = new LoggerConfiguration()
			   .ReadFrom.Configuration(Configuration)
			   .CreateLogger();

			app.UseCors("CorsPolicy");
			app.UseMvc();
			app.UseDefaultFiles();
			app.UseStaticFiles();

			app.UseSwagger();

			app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Back End API"));

			app.RegisterWithConsul(lifetime);
		}
	}
}
