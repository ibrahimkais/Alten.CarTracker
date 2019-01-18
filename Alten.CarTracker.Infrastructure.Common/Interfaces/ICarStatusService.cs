using Alten.CarTracker.Infrastructure.Common.Commands;
using System.Threading.Tasks;

namespace Alten.CarTracker.Infrastructure.Common.Interfaces
{
	public interface ICarStatusService
	{
		Task AcquireStatus(UpdateStatus status);

		Task Ping();
	}
}
