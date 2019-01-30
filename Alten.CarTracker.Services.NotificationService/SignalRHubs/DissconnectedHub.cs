using Alten.CarTracker.Infrastructure.Common.DTOs;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace Alten.CarTracker.Services.NotificationService.SignalRHubs
{
	public class DissconnectedHub : Hub
	{
		public Task SendStatus(VehicleDisconnectedDTO message) => Clients.All.SendAsync("vehicledissconnected", message);
	}
}
