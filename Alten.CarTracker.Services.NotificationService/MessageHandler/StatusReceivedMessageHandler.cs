using Alten.CarTracker.Infrastructure.Common.DTOs;
using Alten.CarTracker.Infrastructure.Common.Events;
using Alten.CarTracker.Infrastructure.Messaging;
using Alten.CarTracker.Services.NotificationService.SignalRHubs;
using AutoMapper;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;

namespace Alten.CarTracker.Services.NotificationService.MessageHandler
{
	public class StatusReceivedMessageHandler : IMessageHandlerCallback
	{
		private readonly IMapper _mapper;
		private readonly IHubContext<StatusHub> _statusHubContext;
		private readonly IHubContext<DissconnectedHub> _disconnectedHubContext;

		public StatusReceivedMessageHandler(IMapper mapper, IHubContext<StatusHub> statusHubContext, IHubContext<DissconnectedHub> disconnectedContext)
		{
			_mapper = mapper;
			_statusHubContext = statusHubContext;
			_disconnectedHubContext = disconnectedContext;
		}

		public void Start()
		{
			//_messageHandler.Start(this);
		}

		public void Stop()
		{
			//_messageHandler.Stop();
		}

		public async Task<bool> HandleMessageAsync(string messageType, string message)
		{
			JObject messageObject = MessageSerializer.Deserialize(message);
			switch (messageType)
			{
				case "StatusReceived":
					await HandleAsync(messageObject.ToObject<StatusReceived>());
					break;
				case "VehicleDisconnected":
					await HandleAsync(messageObject.ToObject<VehicleDisconnected>());
					break;
			}
			return true;
		}

		private async Task<bool> HandleAsync(StatusReceived message)
		{
			StatusReceivedDTO status = _mapper.Map<StatusReceived, StatusReceivedDTO>(message);
			await _statusHubContext.Clients.All.SendAsync("sendstatus", status);
			return true;
		}

		private async Task<bool> HandleAsync(VehicleDisconnected message)
		{
			VehicleDisconnectedDTO status = _mapper.Map<VehicleDisconnected, VehicleDisconnectedDTO>(message);
			await _disconnectedHubContext.Clients.All.SendAsync("vehicledissconnected", status);
			return true;
		}
	}
}
