using Alten.CarTracker.Infrastructure.Common.Commands;
using Alten.CarTracker.Infrastructure.Common.Events;
using Alten.CarTracker.Infrastructure.Messaging;
using Alten.CarTracker.Services.StatusReceivedService.Application;
using Alten.CarTracker.Services.StatusReceivedService.DataAccess;
using Alten.CarTracker.Services.StatusReceivedService.Domain.Model;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Alten.CarTracker.Services.StatusReceivedService.MessageHandler
{
	public class MinutHasPassedMessageHandler : IMessageHandlerCallback
	{
		private readonly IMessagePublisher _messagePublisher;
		private readonly IMapper _mapper;
		private readonly EfDbContext _dbContext;

		private List<string> CarIds { get; set; }

		public MinutHasPassedMessageHandler(IMapper mapper,
			IMessagePublisher messagePublisher, EfDbContext efDbContext)
		{
			_messagePublisher = messagePublisher;
			_mapper = mapper;
			_dbContext = efDbContext;

			GetCarIds().Wait();
		}

		public void Start()
		{
			//_messageHandler.Start(this);
		}

		public void Stop()
		{
			//_messageHandler.Stop();
		}

		public async Task GetCarIds()
		{
			CarIds = await _dbContext.Set<Car>().Select(c => c.Id).ToListAsync();
		}

		public async Task<bool> HandleMessageAsync(string messageType, string message)
		{
			JObject messageObject = MessageSerializer.Deserialize(message);
			Console.WriteLine($"Message Received: MessageType {messageType}");
			Log.Information($"Message Received: MessageType {messageType}");
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
			Console.WriteLine("Minute Message Handled");
			Log.Information("Minute Message Handled");
			Dictionary<string, UpdateStatus> checkList = StatusCheckList.Instance.StartCheck();

			foreach (var command in checkList)
			{
				StatusReceived statusReceived = _mapper.Map<UpdateStatus, StatusReceived>(command.Value);
				await _messagePublisher.PublishMessageAsync(statusReceived.MessageType, statusReceived, "StatusReceived");
				Log.Information($"Status Message published to Message Queue for {statusReceived.VinCode}");
			}

			foreach (var item in CarIds)
			{
				if (string.IsNullOrWhiteSpace(checkList.Keys.FirstOrDefault(c => c == item)))
				{
					VehicleDisconnected vehicleDissconnected = new VehicleDisconnected(Guid.NewGuid(), item);
					await _messagePublisher.PublishMessageAsync(vehicleDissconnected.MessageType, vehicleDissconnected, "StatusReceived");
					Log.Information($"Disconnect Message published to Message Queue for {vehicleDissconnected.VinCode}");
				}
			}

			return true;
		}
	}
}
