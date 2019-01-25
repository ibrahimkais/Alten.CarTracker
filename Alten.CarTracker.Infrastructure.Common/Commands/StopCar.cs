using Alten.CarTracker.Infrastructure.Messaging;
using System;

namespace Alten.CarTracker.Infrastructure.Common.Commands
{
	public class StopCar : Command
	{
		public string VinCode { get; set; }

		public StopCar(Guid messageId, string vinCode) : base(messageId) => VinCode = vinCode;
	}
}
