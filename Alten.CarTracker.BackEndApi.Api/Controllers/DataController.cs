using Alten.CarTracker.BackEndApi.Application;
using Alten.CarTracker.BackEndApi.Application.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Alten.CarTracker.BackEndApi.Api.Controllers
{
	[Route("api/[controller]/[action]")]
	[ApiController]
	public class DataController : ControllerBase
	{
		private readonly ICustomerService _customerService;
		private readonly ICarStatusService _carStatusService;
		private readonly IVehicleService _vehicleService;

		public DataController(ICustomerService customerService, ICarStatusService carStatusService, IVehicleService vehicleService)
		{
			_customerService = customerService;
			_vehicleService = vehicleService;
			_carStatusService = carStatusService;
		}

		[HttpGet]
		public async Task<ActionResult<IEnumerable<CustomerDTO>>> GetCustomers()
		{
			IList<CustomerDTO> customers = await _customerService.Get();
			return customers.ToList();
		}

		[HttpGet]
		public async Task<ActionResult<IEnumerable<VehicleDTO>>> GetVehicles()
		{
			IList<VehicleDTO> vehicles = await _vehicleService.Get();
			return vehicles.ToList();
		}

		[HttpGet("{customerId}")]
		public async Task<ActionResult<IEnumerable<VehicleDTO>>> GetVehiclesForCustomer(int customerId)
		{
			IList<VehicleDTO> vehicles = await _vehicleService.Get(customerId);
			return vehicles.ToList();
		}
	}
}
