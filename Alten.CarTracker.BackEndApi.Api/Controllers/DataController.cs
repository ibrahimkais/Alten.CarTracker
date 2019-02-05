using Alten.CarTracker.BackEndApi.Application;
using Alten.CarTracker.BackEndApi.Application.DTOs;
using Microsoft.AspNetCore.Mvc;
using Serilog;
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
		private readonly IVehicleService _vehicleService;

		public DataController(ICustomerService customerService, IVehicleService vehicleService)
		{
			_customerService = customerService;
			_vehicleService = vehicleService;
		}

		[HttpGet]
		public async Task<ActionResult<IEnumerable<CustomerDTO>>> GetCustomers()
		{
			Log.Information("Request for getting Customers");
			IList<CustomerDTO> customers = await _customerService.Get();
			return customers.ToList();
		}

		[HttpGet]
		public async Task<ActionResult<IEnumerable<VehicleDTO>>> GetVehicles()
		{
			Log.Information("Request for getting Vehicles");
			IList<VehicleDTO> vehicles = await _vehicleService.Get();
			return vehicles.ToList();
		}

		[HttpGet("{customerId}")]
		public async Task<ActionResult<IEnumerable<VehicleDTO>>> GetVehiclesForCustomer(int customerId)
		{
			Log.Information($"Request for getting Vehicles for a Customer {customerId}");
			IList<VehicleDTO> vehicles = await _vehicleService.Get(customerId);
			return vehicles.ToList();
		}
	}
}
