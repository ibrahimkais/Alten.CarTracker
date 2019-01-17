using Alten.CarTracker.BackEndApi.Domain.Model;
using Microsoft.EntityFrameworkCore;
using Polly;
using System;

namespace Alten.CarTracker.CustomersService.DataAccess
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

			builder.Entity<Customer>().HasData(
				new Customer()
				{
					Id = 1,
					Name = "Kalles Grustransporter AB",
					Address = "Cementvägen 8, 111 11 Södertälje"
				},
				new Customer()
				{
					Id = 2,
					Name = "Johans Bulk AB",
					Address = "Balkvägen 12, 222 22 Stockholm"
				},
				new Customer()
				{
					Id = 3,
					Name = "Haralds Värdetransporter AB",
					Address = "Budgetvägen 1, 333 33 Uppsala"
				}
			);

			builder.Entity<Car>().HasData(
				new Car()
				{
					Id = "YS2R4X20005399401",
					RegistrationNumber = "ABC123",
					CustomerId = 1
				},
				new Car()
				{
					Id = "VLUR4X20009093588",
					RegistrationNumber = "DEF456",
					CustomerId = 1
				},
				new Car()
				{
					Id = "VLUR4X20009048044",
					RegistrationNumber = "GHI789",
					CustomerId = 1
				},

				new Car()
				{
					Id = "YS2R4X20005388011",
					RegistrationNumber = "JKL012",
					CustomerId = 2
				},
				new Car()
				{
					Id = "YS2R4X20005387949",
					RegistrationNumber = "MNO345",
					CustomerId = 2
				},

				new Car()
				{
					Id = "VLUR4X20009048066",
					RegistrationNumber = "PQR678",
					CustomerId = 3
				},
				new Car()
				{
					Id = "YS2R4X20005387055",
					RegistrationNumber = "STU901",
					CustomerId = 3
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
					Name = "Connected"
				},
				new CarStatusLookup()
				{
					Id = 4,
					Name = "Disconnected"
				}
			);

			base.OnModelCreating(builder);
		}
	}
}
