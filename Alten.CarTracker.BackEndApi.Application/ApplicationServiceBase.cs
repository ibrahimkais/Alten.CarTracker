using Alten.CarTracker.BackEndApi.DataAccess.Utilities;
using Alten.CarTracker.Infrastructure.Common.Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
namespace Alten.CarTracker.BackEndApi.Application
{
	public class ApplicationServiceBase<T, R> : IApplicationServiceBase<T, R> where T : BaseEntity<R>
	{
		protected readonly IUnitOfWork _unitOfWork;
		protected readonly IRepository<T> _repository;
		public ApplicationServiceBase(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
			_repository = _unitOfWork.GetRepository<T>();
		}

		public virtual async Task<T> Find(R id) => await _repository.FindAsync(id);

		public virtual async Task<IPagedList<T>> Get()
		{
			int count = _repository.Count();
			if (count > 0)
			{
				return await _repository.GetPagedListAsync(null, null, null, 0, count);
			}

			return null;
		}

		public virtual async Task<IPagedList<T>> Get(Expression<Func<T, bool>> predicate)
		{
			int count = _repository.Count();
			if (count > 0)
			{
				return await _repository.GetPagedListAsync(predicate, null, null, 0, count);
			}

			return null;
		}

		public virtual async Task<IPagedList<T>> Get(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IIncludableQueryable<T, object>> includes)
		{
			int count = _repository.Count();
			if (count > 0)
			{
				return await _repository.GetPagedListAsync(predicate, null, includes, 0, count);
			}

			return null;
		}

		/// <summary>
		/// This function should be implemented in a child service
		/// </summary>
		/// <param name="filters"></param>
		/// <param name="pageIndex"></param>
		/// <param name="sortOrder"></param>
		/// <returns>Task<PaginatedList<T>></returns>
		/// <exception cref="System.NotImplementedException">Throw when call this function</exception>
		public virtual Task<PaginatedList<T>> GetPaged(Dictionary<string, dynamic> filters, int pageIndex = 0, string sortOrder = "", int pageSize = 20)
		{
			throw new NotImplementedException("This function should be implemented in a child service");
		}

		public virtual async Task<PaginatedList<T>> GetPaged(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy,
			Func<IQueryable<T>, IIncludableQueryable<T, object>> include, int page = 0, int pageSize = 10)
		{
			IPagedList<T> pagedList = await _repository.GetPagedListAsync(predicate, orderBy, include, page - 1, pageSize);
			return new PaginatedList<T>(pagedList.Items, pagedList.TotalCount, page, pagedList.PageSize);
		}

		public virtual async Task<int> Add(T entity)
		{
			await _repository.InsertAsync(entity);
			return await _unitOfWork.SaveChangesAsync();
		}

		public virtual async Task<int> Update(T entity)
		{
			_repository.Update(entity);
			return await _unitOfWork.SaveChangesAsync();
		}

		public virtual async Task<int> Delete(T entity)
		{
			_repository.Delete(entity);
			return await _unitOfWork.SaveChangesAsync();
		}
	}
}