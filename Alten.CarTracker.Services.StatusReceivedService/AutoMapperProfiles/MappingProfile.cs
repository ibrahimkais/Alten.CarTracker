using Alten.CarTracker.Infrastructure.Common.Commands;
using Alten.CarTracker.Infrastructure.Common.Events;
using Alten.CarTracker.Services.StatusReceivedService.Domain.Model;
using AutoMapper;
using NetTopologySuite.Geometries;
using System;

namespace Alten.CarTracker.Services.StatusReceivedService.AutoMapperProfiles
{
	public class MappingProfile : Profile
	{
		public MappingProfile()
		{
			CreateMap<UpdateStatus, Status>()
				.ForMember(dest => dest.CarId, opt => opt.MapFrom(s => s.VinCode))
				.ForMember(dest => dest.Location, opt => opt.MapFrom<PointResolver, UpdateStatus>(s => s));

			CreateMap<UpdateStatus, StatusReceived>()
				.ForCtorParam("messageId", opt => opt.MapFrom(c => Guid.NewGuid()));
		}
	}

	public class PointResolver : IMemberValueResolver<object, object, UpdateStatus, Point>
	{
		public Point Resolve(object source, object destination, UpdateStatus sourceMember, Point destinationMember, ResolutionContext context)
		{
			return new Point(sourceMember.X, sourceMember.Y);
		}
	}
}
