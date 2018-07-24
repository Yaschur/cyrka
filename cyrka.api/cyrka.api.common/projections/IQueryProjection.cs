using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace cyrka.api.common.projections
{
	public interface IQueryableProjection<TView>
		where TView : IView
	{
		IQueryable<TView> AsQueryable();
	}
}
