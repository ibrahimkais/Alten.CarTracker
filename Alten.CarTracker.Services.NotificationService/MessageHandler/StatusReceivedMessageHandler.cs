using Alten.CarTracker.Infrastructure.Common.DTOs;
using Alten.CarTracker.Infrastructure.Common.Events;
using Alten.CarTracker.Infrastructure.Messaging;
using Alten.CarTracker.Services.NotificationService.SignalRHubs;
using AutoMapper;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json.Linq;
using Serilog;
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
					Log.Information("Status Message Received from message queue");
					await HandleAsync(messageObject.ToObject<StatusReceived>());
					break;
				case "VehicleDisconnected":
					Log.Information("Disconnect Message Received from message queue");
					await HandleAsync(messageObject.ToObject<VehicleDisconnected>());
					break;
			}
			return true;
		}

		private async Task<bool> HandleAsync(StatusReceived message)
		{
			StatusReceivedDTO status = _mapper.Map<StatusReceived, StatusReceivedDTO>(message);
			await _statusHubContext.Clients.All.SendAsync("sendstatus", status);
			Log.Information($"Status Message Sent to UI for {status.VinCode}");
			return true;
		}

		private async Task<bool> HandleAsync(VehicleDisconnected message)
		{
			VehicleDisconnectedDTO status = _mapper.Map<VehicleDisconnected, VehicleDisconnectedDTO>(message);
			await _disconnectedHubContext.Clients.All.SendAsync("vehicledissconnected", status);
			Log.Information($"Disconnect Message Sent to UI for {status.VinCode}");
			return true;
		}
	}
}
