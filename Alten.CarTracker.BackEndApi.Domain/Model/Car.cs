using Alten.CarTracker.Infrastructure.Common.Domain.Model;

namespace Alten.CarTracker.BackEndApi.Domain.Model
{
	public class Car : BaseEntity<string>
	{
		public string RegistrationNumber { get; set; }

		public int CustomerId { get; set; }

		public Customer Customer { get; set; }
	}
}
