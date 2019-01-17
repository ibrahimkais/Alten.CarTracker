using System.Collections.Generic;

namespace Alten.CarTracker.BackEndApi.Application.DTOs
{
	public class CustomerDTO
	{
		public int Id { get; set; }

		public string Name { get; set; }

		public string Address { get; set; }

		public List<VehicleDTO> Vehicles { get; set; } = new List<VehicleDTO>();
	}
}
