using Alten.CarTracker.Infrastructure.Common.Commands;
using Alten.CarTracker.Infrastructure.Common.Interfaces;
using Alten.CarTracker.Services.StatusReceivedService.Application;
using AutoMapper;
using System;

namespace Alten.CarTracker.Services.StatusReceivedService.Controllers
{
	public class CarStatusService : ICarStatusService
	{
		private readonly IMapper _mapper;

		public CarStatusService(IMapper mapper)
		{
			_mapper = mapper;
		}

		public void AcquireStatus(UpdateStatus command)
		{
			StatusCheckList.Instance.ReceviedMessages.Enqueue(command);
		}

		public void Ping()
		{
			throw new NotImplementedException();
		}
	}
}
