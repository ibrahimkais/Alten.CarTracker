using Alten.CarTracker.Infrastructure.Messaging;
using System;

namespace Alten.CarTracker.Infrastructure.Common.Events
{
	public class VehicleDisconnected : Event
	{
		public readonly string VinCode;

		public VehicleDisconnected(Guid messageId, string vinCode) : base(messageId)
		{
			VinCode = vinCode;
		}
	}
}
