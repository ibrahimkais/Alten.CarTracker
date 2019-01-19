using Alten.CarTracker.Infrastructure.Common.Interfaces;
using JKang.IpcServiceFramework;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading;

namespace Alten.CarTracker.Services.StatusReceivedService
{
	public class Program
	{
		public static void Main(string[] args)
		{
			IWebHost webHost = CreateWebHostBuilder(args).Build();

			ThreadPool.QueueUserWorkItem(StartIpcService,
			   webHost.Services.CreateScope().ServiceProvider);

			webHost.Run();
		}

		private static void StartIpcService(object state)
		{
			var serviceProvider = state as IServiceProvider;
			new IpcServiceHostBuilder(serviceProvider)
				.AddNamedPipeEndpoint<ICarStatusService>("carStatusEndPoint", "carStatusPipe")
				.Build()
				.Run();
		}

		public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
			WebHost.CreateDefaultBuilder(args)
				.UseStartup<Startup>();
	}
}
