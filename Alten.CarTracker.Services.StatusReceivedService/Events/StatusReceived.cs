using Alten.CarTracker.Infrastructure.Messaging;
using System;

namespace Alten.CarTracker.Services.StatusReceivedService.Events
{
	public class StatusReceived : Event
	{
		public readonly string VinCode;
		public readonly DateTime ReceivedDate;
		public readonly double X;
		public readonly double Y;
		public readonly int StatusId;

		public StatusReceived(Guid messageId, string vinCode, DateTime receivedDate, double x, double y, int statusId) : base(messageId)
		{
			VinCode = vinCode;
			ReceivedDate = receivedDate;
			X = x;
			Y = y;
			StatusId = statusId;
		}
	}
}
