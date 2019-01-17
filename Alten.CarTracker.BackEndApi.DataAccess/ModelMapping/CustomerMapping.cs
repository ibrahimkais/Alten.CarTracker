using Alten.CarTracker.BackEndApi.Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Alten.CarTracker.BackEndApi.DomainMapping
{
	public class CustomerMapping : IEntityTypeConfiguration<Customer>
	{
		public void Configure(EntityTypeBuilder<Customer> builder)
		{
			builder.HasKey(c => c.Id);
			builder.Property(c => c.Id).UseSqlServerIdentityColumn().HasColumnName("pkCustomerId");
			builder.Property(c => c.Name).IsRequired().IsUnicode().HasMaxLength(150);
			builder.Property(c => c.Address).IsRequired(false).IsUnicode().HasMaxLength(350);

			builder.ToTable("Customers");
		}
	}
}
