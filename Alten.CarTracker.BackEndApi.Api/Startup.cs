using Alten.CarTracker.BackEndApi.Application;
using Alten.CarTracker.BackEndApi.Application.AutoMapperProfiles;
using Alten.CarTracker.CustomersService.DataAccess;
using Alten.CarTracker.Infrastructure.Messaging;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

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

			services.AddTransient<IMessagePublisher>((sp) => new RabbitMQMessagePublisher(host, userName, password, exchange));

			services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

			services.AddScoped<ICustomerService, CustomerService>();
			services.AddScoped<IVehicleService, VehicleService>();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env, IApplicationLifetime lifetime)
		{
			app.UseCors("CorsPolicy");
			app.UseMvc();
			app.UseDefaultFiles();
			app.UseStaticFiles();
		}
	}
}
