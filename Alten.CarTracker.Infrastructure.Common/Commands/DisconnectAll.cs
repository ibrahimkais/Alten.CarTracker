using Alten.CarTracker.Infrastructure.Messaging;
using System;

namespace Alten.CarTracker.Infrastructure.Common.Commands
{
	public class DisconnectAll : Command
	{
		public DisconnectAll(Guid messgeId) : base(messgeId)
		{

		}
	}
}
