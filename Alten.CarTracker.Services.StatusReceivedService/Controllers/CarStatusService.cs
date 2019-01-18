using Alten.CarTracker.Infrastructure.Common.Commands;
using Alten.CarTracker.Infrastructure.Common.Events;
using Alten.CarTracker.Infrastructure.Common.Interfaces;
using Alten.CarTracker.Infrastructure.Messaging;
using Alten.CarTracker.Services.StatusReceivedService.DataAccess;
using Alten.CarTracker.Services.StatusReceivedService.Domain.Model;
using AutoMapper;
using System;
using System.Threading.Tasks;

namespace Alten.CarTracker.Services.StatusReceivedService.Controllers
{
	public class CarStatusService : ICarStatusService
	{
		private readonly IMessagePublisher _messagePublisher;
		private readonly EfDbContext _dbContext;
		private readonly IMapper _mapper;

		public CarStatusService(EfDbContext efDbContext, IMessagePublisher messagePublisher, IMapper mapper)
		{
			_dbContext = efDbContext;
			_messagePublisher = messagePublisher;
			_mapper = mapper;
		}

		public async Task AcquireStatus(UpdateStatus command)
		{
			Status status = _mapper.Map<UpdateStatus, Status>(command);
			_dbContext.Add(status);
			int result = await _dbContext.SaveChangesAsync();

			// send event
			StatusReceived message = Mapper.Map<StatusReceived>(command);
			await _messagePublisher.PublishMessageAsync(message.MessageType, message, "StatusReceived");
		}

		public Task Ping()
		{
			throw new NotImplementedException();
		}
	}
}
