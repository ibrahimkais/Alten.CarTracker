using Alten.CarTracker.Services.StatusReceivedService.Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Alten.CarTracker.Services.StatusReceivedService.DataAccess.Mapping
{
	public class CarStatusLookupMapping : IEntityTypeConfiguration<CarStatusLookup>
	{
		public void Configure(EntityTypeBuilder<CarStatusLookup> builder)
		{
			builder.HasKey(c => c.Id);
			builder.Property(c => c.Id).UseSqlServerIdentityColumn().HasColumnName("pkCarStatusLookupId");
			builder.Property(c => c.Name).IsRequired().IsUnicode().HasMaxLength(20);

			builder.ToTable("CarStatusesLookup");
		}
	}
}
