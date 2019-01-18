using Alten.CarTracker.Infrastructure.Common.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace Alten.CarTracker.Infrastructure.Common.Interfaces
{
	public interface ICarStatusService
	{
		void SendStatus(UpdateStatus status);

		void Ping();
	}
}
