using Alten.CarTracker.Infrastructure.Messaging;
using System;

namespace Alten.CarTracker.Infrastructure.Common.Commands
{
	public class StopAll : Command
	{
		public StopAll(Guid messageId) : base(messageId)
		{

		}
	}
}
