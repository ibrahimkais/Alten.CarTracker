using Alten.CarTracker.BackEndApi.Application.DTOs;
using Alten.CarTracker.BackEndApi.Application.Exceptions;
using Alten.CarTracker.BackEndApi.Domain.Model;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Alten.CarTracker.BackEndApi.Application
{
	public class VehicleService : ApplicationServiceBase<Car, string>, IVehicleService
	{
		private readonly IRepository<Car> _carsRepository;
		private readonly IMapper _mapper;

		public VehicleService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork)
		{
			_carsRepository = _unitOfWork.GetRepository<Car>();
			_mapper = mapper;
		}

		public new async Task<IList<VehicleDTO>> Get()
		{
			IPagedList<Car> cars = await base.Get();
			if (cars != null && cars.TotalCount > 0)
			{
				return _mapper.Map<IList<Car>, IList<VehicleDTO>>(cars.Items);
			}
			return new List<VehicleDTO>();
		}

		public async Task<IList<VehicleDTO>> Get(int CustomerId)
		{
			if (CustomerId <= 0)
			{
				throw new CustomerIdException($"Customer Id is {CustomerId}");
			}

			IPagedList<Car> cars = await base.Get(c => c.CustomerId == CustomerId);
			if (cars != null && cars.TotalCount > 0)
			{
				return _mapper.Map<IList<Car>, IList<VehicleDTO>>(cars.Items);
			}
			return new List<VehicleDTO>();
		}
	}
}