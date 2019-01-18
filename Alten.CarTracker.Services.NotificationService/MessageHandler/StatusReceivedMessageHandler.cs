using Alten.CarTracker.Infrastructure.Common.Events;
using Alten.CarTracker.Infrastructure.Messaging;
using Alten.CarTracker.Services.NotificationService.DTOs;
using Alten.CarTracker.Services.NotificationService.SignalRHubs;
using AutoMapper;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;

namespace Alten.CarTracker.Services.NotificationService.MessageHandler
{
	public class StatusReceivedMessageHandler : IMessageHandlerCallback
	{
		private readonly IMessageHandler _messageHandler;
		private readonly IMapper _mapper;
		private readonly IHubContext<FrontNotificationHub> _hubContext;

		public StatusReceivedMessageHandler(IMessageHandler messageHandler, IMapper mapper, IHubContext<FrontNotificationHub> hubContext)
		{
			_messageHandler = messageHandler;
			_mapper = mapper;
			_hubContext = hubContext;

			_messageHandler.Start(this);
		}

		public void Start()
		{
			_messageHandler.Start(this);
		}

		public void Stop()
		{
			_messageHandler.Stop();
		}

		public async Task<bool> HandleMessageAsync(string messageType, string message)
		{
			JObject messageObject = MessageSerializer.Deserialize(message);
			switch (messageType)
			{
				case "CustomerRegistered":
					await HandleAsync(messageObject.ToObject<StatusReceived>());
					break;
			}
			return true;
		}

		private async Task<bool> HandleAsync(StatusReceived message)
		{
			StatusReceivedDTO status = _mapper.Map<StatusReceived, StatusReceivedDTO>(message);
			await _hubContext.Clients.All.SendAsync("SendMessageToGroup", status);
			return true;
		}
	}
}
