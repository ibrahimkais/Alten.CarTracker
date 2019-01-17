using Alten.CarTracker.Services.StatusReceivedService.Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Alten.CarTracker.Services.StatusReceivedService.DataAccess.Mapping
{
	public class CarMapping : IEntityTypeConfiguration<Car>
	{
		public void Configure(EntityTypeBuilder<Car> builder)
		{
			builder.HasKey(c => c.Id);
			builder.Property(c => c.Id).HasMaxLength(25).HasColumnName("pkCarId");

			builder.ToTable("Cars");
		}
	}
}
