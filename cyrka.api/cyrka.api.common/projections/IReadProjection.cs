using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace cyrka.api.common.projections
{
	public interface IReadProjection<TView>
		where TView : IView
	{
		TView[] Find(Expression<Func<TView, bool>> predicate);

		TView GetById(string id);
	}
}
