using Alten.CarTracker.Infrastructure.Common.Commands;
using Alten.CarTracker.Infrastructure.Messaging;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Alten.CarTracker.Simulation.Simulator.MessageHandler
{
	public class CarMessageHandler : IMessageHandlerCallback
	{
		private readonly IMessageHandler _messageHandler;
		private readonly List<CarManager> _carManagers;

		public CarMessageHandler(IMessageHandler messageHandler)
		{
			_messageHandler = messageHandler;
			_carManagers = new List<CarManager>();
		}

		public void Start() => _messageHandler.Start(this);

		public void Stop() => _messageHandler.Stop();

		public void Register(CarManager timeManager) => _carManagers.Add(timeManager);

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
					case "Reset":
						Handle(messageObject.ToObject<Reset>());
						break;
				}
				return true;
			});
		}

		private void Handle(Reset reset) => _carManagers.ForEach(manager => manager.Reset());

		private void Handle(DisconnectAll disconnectAll) => _carManagers.ForEach(manager => manager.Disconnect(manager.Car.VinCode));

		private void Handle(StopAll stopAll) => _carManagers.ForEach(manager => manager.Stop(manager.Car.VinCode));

		private void Handle(StartAll startAll) => _carManagers.ForEach(async manager => await manager.Start(manager.Car.VinCode));

		private void Handle(DisconnectCar disconnectCar) => _carManagers.ForEach(manager => manager.Disconnect(disconnectCar.VinCode));

		private void Handle(StopCar stopCar) => _carManagers.ForEach(manager => manager.Stop(stopCar.VinCode));

		private void Handle(StartCar startCar) => _carManagers.ForEach(async manager => await manager.Start(startCar.VinCode));
	}
}
