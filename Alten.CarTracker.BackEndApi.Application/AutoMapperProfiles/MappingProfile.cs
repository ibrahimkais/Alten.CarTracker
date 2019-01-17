using Alten.CarTracker.BackEndApi.Application.DTOs;
using Alten.CarTracker.BackEndApi.Domain.Model;
using AutoMapper;

namespace Alten.CarTracker.BackEndApi.Application.AutoMapperProfiles
{
	public class MappingProfile : Profile
	{
		public MappingProfile()
		{
			CreateMap<Car, VehicleDTO>()
				.ForMember(dest => dest.VIN, opt => opt.MapFrom(c => c.Id))
				.ForMember(dest => dest.RegistrationNumber, opt => opt.MapFrom(c => c.RegistrationNumber))
				.ReverseMap();

			CreateMap<Customer, CustomerDTO>()
				.ForMember(dest => dest.Vehicles, opt => opt.MapFrom(c => c.Cars))
				.ReverseMap();
		}
	}
}
