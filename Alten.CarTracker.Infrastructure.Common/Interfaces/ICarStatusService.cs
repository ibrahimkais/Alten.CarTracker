using Alten.CarTracker.Infrastructure.Common.Commands;
using System.Threading.Tasks;

namespace Alten.CarTracker.Infrastructure.Common.Interfaces
{
	public interface ICarStatusService
	{
		void AcquireStatus(UpdateStatus status);

		void Ping();
	}
}
