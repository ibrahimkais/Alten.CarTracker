using Alten.CarTracker.BackEndApi.Application.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Alten.CarTracker.BackEndApi.Application
{
	public interface IVehicleService
	{
		Task<IList<VehicleDTO>> Get();
		Task<IList<VehicleDTO>> Get(int CustomerId);
	}
}