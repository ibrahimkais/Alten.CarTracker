using Alten.CarTracker.Infrastructure.Common.Commands;
using Alten.CarTracker.Infrastructure.Messaging;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace Alten.CarTracker.Simulation.Simulator.MessageHandler
{
	public class CarMessageHandler : IMessageHandlerCallback
	{
		private readonly IMessageHandler _messageHandler;
		private readonly List<TimeManager> _timeManagers;

		public CarMessageHandler(IMessageHandler messageHandler)
		{
			_messageHandler = messageHandler;
			_timeManagers = new List<TimeManager>();
		}

		public void Start() => _messageHandler.Start(this);

		public void Stop() => _messageHandler.Stop();

		public void Register(TimeManager timeManager) => _timeManagers.Add(timeManager);

		public Task<bool> HandleMessageAsync(string messageType, string message)
		{
			return Task.Run(() =>
			{
				JObject messageObject = MessageSerializer.Deserialize(message);
				Console.WriteLine($"Message Received: MessageType {messageType}");
				switch (messageType)
				{
					case "StartCar":
						Handle(messageObject.ToObject<StartCar>());
						break;
					case "StopCar":
						Handle(messageObject.ToObject<StopCar>());
						break;
					case "DisconnectCar":
						Handle(messageObject.ToObject<DisconnectCar>());
						break;
					case "StartAll":
						Handle(messageObject.ToObject<StartAll>());
						break;
					case "StopAll":
						Handle(messageObject.ToObject<StopAll>());
						break;
					case "DisconnectAll":
						Handle(messageObject.ToObject<DisconnectAll>());
						break;
				}
				return true;
			});
		}

		private void Handle(DisconnectAll disconnectAll)
		{
			_timeManagers.ForEach(manager => manager.Disconnect(manager.Car.VinCode));
		}

		private void Handle(StopAll stopAll)
		{
			_timeManagers.ForEach(manager => manager.Stop(manager.Car.VinCode));
		}

		private void Handle(StartAll startAll)
		{
			_timeManagers.ForEach(async manager => await manager.Start(manager.Car.VinCode));
		}

		private void Handle(DisconnectCar disconnectCar) => _timeManagers.ForEach(manager => manager.Disconnect(disconnectCar.VinCode));

		private void Handle(StopCar stopCar) => _timeManagers.ForEach(manager => manager.Stop(stopCar.VinCode));

		private void Handle(StartCar startCar) => _timeManagers.ForEach(async manager => await manager.Start(startCar.VinCode));
	}
}
