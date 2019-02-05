using Consul;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;

//https://cecilphillip.com/using-consul-for-service-discovery-with-asp-net-core/
namespace Alten.CarTracker.Infrastructure.ServiceDiscovery
{
	public static class ApplicationBuilderExtensions
	{
		public static IApplicationBuilder RegisterWithConsul(this IApplicationBuilder app, IApplicationLifetime lifetime)
		{
			// Retrieve Consul client from DI
			var consulClient = app.ApplicationServices.GetRequiredService<IConsulClient>();
			var consulConfig = app.ApplicationServices.GetRequiredService<IOptions<ConsulConfig>>();
			// Setup logger
			var loggingFactory = app.ApplicationServices.GetRequiredService<ILoggerFactory>();
			var logger = loggingFactory.CreateLogger<IApplicationBuilder>();

			// Get server IP address
			var hostname = Dns.GetHostName(); // get container id
			var ip = Dns.GetHostEntry(hostname).AddressList.First(x => x.AddressFamily == AddressFamily.InterNetwork).ToString();

			var features = app.Properties["server.Features"] as FeatureCollection;
			var addresses = features.Get<IServerAddressesFeature>();
			string addressB = addresses.Addresses.First();
			var address = addressB.Contains('*') ? addressB.Replace("*", ip) : addressB.Contains('+') ? addressB.Replace("+", ip) : addressB;

			Console.WriteLine($"Address used for Consul registration: {address} for app {consulConfig.Value.ServiceName}");

			var uri = new Uri(address);
			var serviceId = $"{consulConfig.Value.ServiceID}-{hostname}-{uri.Port}";
			var serviceChecks = new List<AgentServiceCheck>();

			if (!string.IsNullOrEmpty(consulConfig.Value.HealthCheckTemplate))
			{
				var healthCheckUri = new Uri(uri, consulConfig.Value.HealthCheckTemplate).OriginalString;
				serviceChecks.Add(new AgentServiceCheck()
				{
					Status = HealthStatus.Passing,
					DeregisterCriticalServiceAfter = TimeSpan.FromMinutes(1),
					Interval = TimeSpan.FromSeconds(5),
					HTTP = healthCheckUri
				});

				logger.LogInformation($"Adding healthcheck for service {serviceId}, checking {healthCheckUri}.");
			}

			// Register service with consul
			var registration = new AgentServiceRegistration()
			{
				Checks = serviceChecks.ToArray(),
				ID = serviceId,
				Name = consulConfig.Value.ServiceName,
				Address = $"{uri.Scheme}://{uri.Host}",
				Port = uri.Port
			};

			logger.LogInformation("Registering with Consul");
			consulClient.Agent.ServiceDeregister(registration.ID).GetAwaiter().GetResult();
			consulClient.Agent.ServiceRegister(registration).GetAwaiter().GetResult();

			lifetime.ApplicationStopping.Register(() =>
			{
				logger.LogInformation("Deregistering from Consul");
				consulClient.Agent.ServiceDeregister(registration.ID).GetAwaiter().GetResult();
			});

			return app;
		}
	}
}