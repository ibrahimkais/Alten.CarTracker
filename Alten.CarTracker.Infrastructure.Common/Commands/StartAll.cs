using Alten.CarTracker.Infrastructure.Messaging;
using System;

namespace Alten.CarTracker.Infrastructure.Common.Commands
{
	public class StartAll : Command
	{

		public StartAll(Guid messageId) : base(messageId)
		{

		}
	}
}
