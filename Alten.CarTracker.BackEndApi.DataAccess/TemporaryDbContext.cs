using Alten.CarTracker.CustomersService.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Alten.CarTracker.BackEndApi.DataAccess
{
	public class TemporaryDbContext : IDesignTimeDbContextFactory<EfDbContext>
	{
		public EfDbContext CreateDbContext(string[] args)
		{
			var builder = new DbContextOptionsBuilder<EfDbContext>();
			builder.UseSqlServer("server=localhost,1434;user id=sa;password=P@ssw0rd;database=CarTracker;MultipleActiveResultSets=True");
			return new EfDbContext(builder.Options);
		}
	}
}