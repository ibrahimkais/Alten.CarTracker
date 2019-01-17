using Alten.CarTracker.Infrastructure.Common.Domain.Model;

namespace Alten.CarTracker.BackEndApi.Domain.Model
{
	public class CarStatusLookup : BaseEntity<int>
	{
		public string Name { get; set; }
	}
}
