using Alten.CarTracker.Infrastructure.Common.DTOs;
using Alten.CarTracker.Infrastructure.Common.Events;
using AutoMapper;

namespace Alten.CarTracker.Services.NotificationService.MappingProfiles
{
	public class MappingProfile : Profile
	{
		public MappingProfile()
		{
			CreateMap<StatusReceived, StatusReceivedDTO>();
		}
	}
}
