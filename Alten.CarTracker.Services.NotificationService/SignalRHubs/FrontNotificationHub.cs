using Alten.CarTracker.Infrastructure.Common.DTOs;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace Alten.CarTracker.Services.NotificationService.SignalRHubs
{
	public class FrontNotificationHub : Hub
	{
		public Task SendMessageToGroup(StatusReceivedDTO message)
		{
			return Clients.Group("FrontUI").SendAsync("StatusMessage", message);
		}
	}
}
