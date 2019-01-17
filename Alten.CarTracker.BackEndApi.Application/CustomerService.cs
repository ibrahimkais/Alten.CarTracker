using Alten.CarTracker.BackEndApi.Application.DTOs;
using Alten.CarTracker.BackEndApi.Domain.Model;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Alten.CarTracker.BackEndApi.Application
{
	public class CustomerService : ApplicationServiceBase<Customer, int>, ICustomerService
	{
		private readonly IRepository<Customer> _customersRepository;
		private readonly IMapper _mapper;

		public CustomerService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork)
		{
			_customersRepository = _unitOfWork.GetRepository<Customer>();
			_mapper = mapper;
		}

		public new async Task<IList<CustomerDTO>> Get()
		{
			IPagedList<Customer> customers = await base.Get();
			if (customers != null && customers.TotalCount > 0)
			{
				return _mapper.Map<IList<Customer>, IList<CustomerDTO>>(customers.Items);
			}
			return new List<CustomerDTO>();
		}
	}
}