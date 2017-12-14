using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace cyrka.api.domain
{
	public interface IQueryStore
	{
		IQueryable<TProjection> AsQueryable<TProjection>();
		Task Upsert<TProjection>(TProjection projectionValue, Expression<Func<TProjection, bool>> filter);
		Task Delete<TProjection>(Expression<Func<TProjection, bool>> filter);
	}
}
