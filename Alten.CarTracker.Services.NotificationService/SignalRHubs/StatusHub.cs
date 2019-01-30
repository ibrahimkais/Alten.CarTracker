using Alten.CarTracker.Infrastructure.Common.DTOs;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace Alten.CarTracker.Services.NotificationService.SignalRHubs
{
	public class StatusHub : Hub
	{
		public Task SendStatus(StatusReceivedDTO message) => Clients.All.SendAsync("sendstatus", message);
	}
}
