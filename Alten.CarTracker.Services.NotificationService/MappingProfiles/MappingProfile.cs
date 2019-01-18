using Alten.CarTracker.Infrastructure.Common.Events;
using Alten.CarTracker.Services.NotificationService.DTOs;
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
