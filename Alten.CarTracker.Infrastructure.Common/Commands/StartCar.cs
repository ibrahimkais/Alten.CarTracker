using Alten.CarTracker.Infrastructure.Messaging;
using System;

namespace Alten.CarTracker.Infrastructure.Common.Commands
{
	public class StartCar : Command
	{
		public string VinCode { get; set; }

		public StartCar(Guid messageId, string vinCode) : base(messageId) => VinCode = vinCode;
	}
}
