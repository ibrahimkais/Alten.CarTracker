using Alten.CarTracker.BackEndApi.Application.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Alten.CarTracker.BackEndApi.Application
{
	public interface ICustomerService
	{
		Task<IList<CustomerDTO>> Get();
	}
}