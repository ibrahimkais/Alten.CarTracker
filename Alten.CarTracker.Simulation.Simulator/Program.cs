using Alten.CarTracker.Infrastructure.Messaging;
using Alten.CarTracker.Simulation.Simulator.Data;
using Alten.CarTracker.Simulation.Simulator.MessageHandler;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace Alten.CarTracker.Simulation.Simulator
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
				.AddJsonFile($"appsettings.Development.json", optional: false)
				.Build();
		}

		static void Main(string[] args)
		{
			var configSection = Config.GetSection("RabbitMQ");
			string host = configSection["Host"];
			string userName = configSection["UserName"];
			string password = configSection["Password"];
			string exchange = configSection["Exchange"];

			RabbitMQMessageHandler handler = new RabbitMQMessageHandler(
				host,
				userName,
				password,
				exchange,
				"CarStatus",
				"CarStatus");

			configSection = Config.GetSection("StatusService");

			string baseAddress = configSection["baseAddress"];
			string controller = configSection["controller"];
			string action = configSection["action"];

			CarMessageHandler carMessageHandler = new CarMessageHandler(handler);

			var serviceProvider = new ServiceCollection()
				.AddHttpClient()
				.BuildServiceProvider();

			var httpClientFactory = serviceProvider.GetService<IHttpClientFactory>();

			var client = httpClientFactory.CreateClient();

			client.BaseAddress = new Uri(baseAddress);
			client.DefaultRequestHeaders.Add("Accept", "application/json");

			Parallel.ForEach(DataLoader.Instance.Cars, (car) =>
			{
				TimeManager timeManager = new TimeManager(car, client, controller, action);
				carMessageHandler.Register(timeManager);
			});

			carMessageHandler.Start();
			
			Console.ReadLine();
		}
	}
}
