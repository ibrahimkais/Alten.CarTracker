using Alten.CarTracker.BackEndApi.Application.DTOs;
using Alten.CarTracker.BackEndApi.Domain.Model;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Alten.CarTracker.BackEndApi.Application
{
	public class CarStatusService : ApplicationServiceBase<CarStatusLookup, int>, ICarStatusService
	{
		private readonly IRepository<CarStatusLookup> _carStatusRepository;
		private readonly IMapper _mapper;

		public CarStatusService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork)
		{
			_carStatusRepository = _unitOfWork.GetRepository<CarStatusLookup>();
			_mapper = mapper;
		}

		public new async Task<IList<CarStatusDTO>> Get()
		{
			IPagedList<CarStatusLookup> carStatuses = await base.Get();
			if (carStatuses != null && carStatuses.TotalCount > 0)
			{
				return _mapper.Map<IList<CarStatusLookup>, IList<CarStatusDTO>>(carStatuses.Items);
			}
			return new List<CarStatusDTO>();
		}
	}
}