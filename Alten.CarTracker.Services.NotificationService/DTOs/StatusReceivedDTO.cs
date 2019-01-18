using System;

namespace Alten.CarTracker.Services.NotificationService.DTOs
{
	public class StatusReceivedDTO
	{
		public string VinCode { get; set; }

		public DateTime ReceivedDate { get; set; }

		public double X { get; set; }

		public double Y { get; set; }

		public int StatusId { get; set; }
	}
}
