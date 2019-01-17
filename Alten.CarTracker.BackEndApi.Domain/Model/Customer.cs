using Alten.CarTracker.Infrastructure.Common.Domain.Model;
using System.Collections.Generic;

namespace Alten.CarTracker.BackEndApi.Domain.Model
{
	public class Customer : BaseEntity<int>
	{
		public string Name { get; set; }

		public string Address { get; set; }

		public List<Car> Cars { get; set; } = new List<Car>();
	}
}
