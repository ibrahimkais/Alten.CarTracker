using Alten.CarTracker.Infrastructure.Common.Events;
using Alten.CarTracker.Infrastructure.Messaging;
using AutoMapper;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;

namespace Alten.CarTracker.Services.StatusReceivedService.MessageHandler
{
	public class MinutHasPassedMessageHandler : IMessageHandlerCallback
	{
		private readonly IMessageHandler _messageHandler;
		private readonly IMapper _mapper;

		public MinutHasPassedMessageHandler(IMessageHandler messageHandler, IMapper mapper)
		{
			_messageHandler = messageHandler;
			_mapper = mapper;

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
				case "MinuteHasPassed":
					await HandleAsync(messageObject.ToObject<MinuteHasPassed>());
					break;
			}
			return true;
		}

		private async Task<bool> HandleAsync(MinuteHasPassed message)
		{

			return true;
		}
	}
}
