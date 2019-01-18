using Alten.CarTracker.Infrastructure.Messaging;
using System;

namespace Alten.CarTracker.Infrastructure.Common.Events
{
	public class MinuteHasPassed : Event
	{
		public MinuteHasPassed(Guid messageId) : base(messageId)
		{

		}
	}
}
