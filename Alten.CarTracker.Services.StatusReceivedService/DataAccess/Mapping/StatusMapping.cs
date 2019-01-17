using Alten.CarTracker.Services.StatusReceivedService.Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Alten.CarTracker.Services.StatusReceivedService.DataAccess.Mapping
{
	public class StatusMapping : IEntityTypeConfiguration<Status>
	{
		public void Configure(EntityTypeBuilder<Status> builder)
		{
			builder.HasKey(s => s.Id);
			builder.Property(s => s.Id).UseSqlServerIdentityColumn().HasColumnName("pkStatusId");
			builder.Property(s => s.StatusId).IsRequired();
			builder.Property(s => s.Location).IsRequired();
			builder.Property(s => s.ReceivedDate).IsRequired().HasColumnType("datetime");
			builder.Property(s => s.CarId).IsRequired();

			builder.HasOne(s => s.Car).WithMany().HasForeignKey(s => s.CarId).OnDelete(DeleteBehavior.Cascade);
			builder.HasOne(s => s.CarStatus).WithMany().HasForeignKey(s => s.StatusId).OnDelete(DeleteBehavior.Cascade);

			builder.ToTable("CarStatuses");
		}
	}
}
