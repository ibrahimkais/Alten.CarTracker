using Alten.CarTracker.Services.StatusReceivedService.Domain.Model;
using Microsoft.EntityFrameworkCore;
using Polly;
using System;

namespace Alten.CarTracker.Services.StatusReceivedService.DataAccess
{
	public class EfDbContext : DbContext
	{
		public EfDbContext(DbContextOptions<EfDbContext> options) : base(options)
		{
			Policy
				.Handle<Exception>()
				.WaitAndRetry(5, r => TimeSpan.FromSeconds(5))
				.Execute(() => Database.Migrate());
		}

		protected override void OnModelCreating(ModelBuilder builder)
		{
			builder.ApplyConfigurationsFromAssembly(GetType().Assembly);

			builder.Entity<Car>().HasData(
				new Car()
				{
					Id = "YS2R4X20005399401",
				},
				new Car()
				{
					Id = "VLUR4X20009093588",
				},
				new Car()
				{
					Id = "VLUR4X20009048044",
				},

				new Car()
				{
					Id = "YS2R4X20005388011",
				},
				new Car()
				{
					Id = "YS2R4X20005387949",
				},

				new Car()
				{
					Id = "VLUR4X20009048066",
				},
				new Car()
				{
					Id = "YS2R4X20005387055",
				}
			);

			builder.Entity<CarStatusLookup>().HasData(
				new CarStatusLookup()
				{
					Id = 1,
					Name = "Stopped"
				},
				new CarStatusLookup()
				{
					Id = 2,
					Name = "Moving"
				},
				new CarStatusLookup()
				{
					Id = 3,
					Name = "Disconnected"
				}
			);

			base.OnModelCreating(builder);
		}
	}
}
