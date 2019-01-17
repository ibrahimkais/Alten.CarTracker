﻿using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Serilog;

namespace Alten.CarTracker.Services.NotificationService
{
	public class Program
	{
		public static void Main(string[] args)
		{
			CreateWebHostBuilder(args).Build().Run();
		}

		public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
			WebHost.CreateDefaultBuilder(args)
			.UseApplicationInsights()
				.UseSerilog()
				.UseHealthChecks("/hc")
				.UseStartup<Startup>();
	}
}
