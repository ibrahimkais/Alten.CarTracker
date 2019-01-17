using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Alten.CarTracker.Services.NotificationService.SignalRHubs
{
	public class FrontNotificationHub: Hub
	{
		public Task SendMessageToGroup(string message)
		{
			return Clients.Group("FrontUI").SendAsync("ReceiveMessage", message);
		}
	}
}
