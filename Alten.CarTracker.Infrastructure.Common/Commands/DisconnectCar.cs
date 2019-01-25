using Alten.CarTracker.Infrastructure.Messaging;
using System;

namespace Alten.CarTracker.Infrastructure.Common.Commands
{
	public class DisconnectCar : Command
	{
		public string VinCode { get; set; }

		public DisconnectCar(Guid messageId, string vinCode) : base(messageId) => VinCode = vinCode;
	}
}
