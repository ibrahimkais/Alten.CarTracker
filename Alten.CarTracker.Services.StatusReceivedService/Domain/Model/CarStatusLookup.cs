using Alten.CarTracker.Infrastructure.Common.Domain.Model;

namespace Alten.CarTracker.Services.StatusReceivedService.Domain.Model
{
	public class CarStatusLookup : BaseEntity<int>
	{
		public string Name { get; set; }
	}
}
