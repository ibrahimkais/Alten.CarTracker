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
	public interface IApplicationServiceBase<T, R> where T : BaseEntity<R>
	{
		Task<int> Add(T entity);
		Task<int> Delete(T entity);
		Task<T> Find(R id);
		Task<IPagedList<T>> Get();
		Task<IPagedList<T>> Get(Expression<Func<T, bool>> predicate);
		Task<IPagedList<T>> Get(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IIncludableQueryable<T, object>> includes);
		/// <summary>
		/// This function should be implemented in a child service
		/// </summary>
		/// <param name="filters"></param>
		/// <param name="pageIndex"></param>
		/// <param name="sortOrder"></param>
		/// <returns>Task<PaginatedList<T>></returns>
		/// <exception cref="System.NotImplementedException">Throw when call this function</exception>
		Task<PaginatedList<T>> GetPaged(Dictionary<string, dynamic> filters, int pageIndex = 0, string sortOrder = "", int pageSize = 20);
		Task<PaginatedList<T>> GetPaged(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy, Func<IQueryable<T>, IIncludableQueryable<T, object>> include, int page = 0, int pageSize = 20);
		Task<int> Update(T entity);
	}
}