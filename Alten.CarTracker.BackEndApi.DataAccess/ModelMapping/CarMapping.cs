using Alten.CarTracker.BackEndApi.Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Alten.CarTracker.Api.ModelMapping
{
	public class CarMapping : IEntityTypeConfiguration<Car>
	{
		public void Configure(EntityTypeBuilder<Car> builder)
		{
			builder.HasKey(c => c.Id);
			builder.Property(c => c.Id).HasMaxLength(25).HasColumnName("pkCarId");
			builder.Property(c => c.RegistrationNumber).IsRequired().IsUnicode().HasMaxLength(10);
			builder.Property(c => c.CustomerId).IsRequired().HasColumnName("fkCustomerId");
			builder.HasOne(c => c.Customer).WithMany(c => c.Cars).HasForeignKey(c => c.CustomerId).OnDelete(DeleteBehavior.Cascade);

			builder.ToTable("Cars");
		}
	}
}
