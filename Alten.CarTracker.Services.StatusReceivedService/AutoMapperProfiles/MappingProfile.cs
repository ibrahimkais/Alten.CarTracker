using Alten.CarTracker.Infrastructure.Common.Commands;
using Alten.CarTracker.Infrastructure.Common.DTOs;
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
				.ForMember(dest => dest.Location, opt => opt.MapFrom<UpdateStatusToPointResolver, UpdateStatus>(s => s));

			CreateMap<UpdateStatus, StatusReceived>()
				.ForCtorParam("messageId", opt => opt.MapFrom(c => Guid.NewGuid()))
				.ForCtorParam("vinCode", opt => opt.MapFrom(c => c.VinCode))
				.ForCtorParam("receivedDate", opt => opt.MapFrom(c => c.ReceivedDate))
				.ForCtorParam("x", opt => opt.MapFrom(c => c.X))
				.ForCtorParam("y", opt => opt.MapFrom(c => c.Y))
				.ForCtorParam("statusId", opt => opt.MapFrom(c => c.StatusId));

			CreateMap<StatusReceivedDTO, Status>()
				.ForMember(dest => dest.CarId, opt => opt.MapFrom(s => s.VinCode))
				.ForMember(dest => dest.Location, opt => opt.MapFrom<StatusReceivedDTOToPointResolver, StatusReceivedDTO>(s => s));

			CreateMap<StatusReceivedDTO, UpdateStatus>()
				.ForCtorParam("messageId", opt => opt.MapFrom(c => Guid.NewGuid()))
				.ForCtorParam("vinCode", opt => opt.MapFrom(c => c.VinCode))
				.ForCtorParam("receivedDate", opt => opt.MapFrom(c => c.ReceivedDate))
				.ForCtorParam("x", opt => opt.MapFrom(c => c.X))
				.ForCtorParam("y", opt => opt.MapFrom(c => c.Y))
				.ForCtorParam("statusId", opt => opt.MapFrom(c => c.StatusId));
		}
	}

	public class UpdateStatusToPointResolver : IMemberValueResolver<object, object, UpdateStatus, Point>
	{
		public Point Resolve(object source, object destination, UpdateStatus sourceMember, Point destinationMember, ResolutionContext context)
		{
			return new Point(sourceMember.X, sourceMember.Y) {SRID = 4326 };
		}
	}

	public class StatusReceivedDTOToPointResolver : IMemberValueResolver<object, object, StatusReceivedDTO, Point>
	{
		public Point Resolve(object source, object destination, StatusReceivedDTO sourceMember, Point destinationMember, ResolutionContext context)
		{
			return new Point(sourceMember.X, sourceMember.Y) { SRID = 4326 };
		}
	}
}
