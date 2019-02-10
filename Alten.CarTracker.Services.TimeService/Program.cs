using Alten.CarTracker.Infrastructure.Messaging;
using Microsoft.Extensions.Configuration;
using Serilog;
using System;
using System.IO;
using System.Threading;

namespace Alten.CarTracker.Services.TimeService
{
	class Program
	{
		private static string _env;
		public static IConfigurationRoot Config { get; private set; }

		static Program()
		{
			_env = Environment.GetEnvironmentVariable("CAR_TRACKER_ENVIRONMENT");

			Config = new ConfigurationBuilder()
				.SetBasePath(Directory.GetCurrentDirectory())
				.AddJsonFile("appsettings.json")
				.AddJsonFile($"appsettings.{_env}.json", optional: false)
				.Build();

			Log.Logger = new LoggerConfiguration()
				.ReadFrom.Configuration(Config)
				.CreateLogger();

			Log.Information($"Environment: {_env}");
		}

		static void Main(string[] args)
		{
			// get configuration
			var configSection = Config.GetSection("RabbitMQ");
			string host = configSection["Host"];
			string userName = configSection["UserName"];
			string password = configSection["Password"];
			string exchange = configSection["Exchange"];

			// start time manager
			RabbitMQMessagePublisher messagePublisher = new RabbitMQMessagePublisher(host, userName, password, exchange);
			TimeManager manager = new TimeManager(messagePublisher);
			manager.Start();
			Log.Information("Time service started.");

			Thread.Sleep(Timeout.Infinite);
		}
	}
}
