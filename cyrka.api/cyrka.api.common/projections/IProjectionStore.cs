using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace cyrka.api.common.projections
{
	public interface IProjectionStore
	{
		IQueryable<TProjection> Query<TProjection>();
		Task Upsert<TProjection>(TProjection projectionValue, Expression<Func<TProjection, bool>> filter);
		Task Delete<TProjection>(Expression<Func<TProjection, bool>> filter);
	}
}
