using Alten.CarTracker.Infrastructure.Messaging;
using System;

namespace Alten.CarTracker.Infrastructure.Common.Commands
{
	public class Reset : Command
	{
		public Reset(Guid messageId) : base(messageId)
		{

		}
	}
}
